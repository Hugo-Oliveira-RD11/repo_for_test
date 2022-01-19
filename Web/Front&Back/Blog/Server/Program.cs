var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));


#region Authentication Config

var secret = new Settings();
var key = Encoding.ASCII.GetBytes(secret.Secret);
builder.Services.AddAuthentication( op =>
    {
        op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(op => 
    {
        op.RequireHttpsMetadata = false;
        op.SaveToken = true;
        op.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
    });
#endregion

#region Authorization Config

builder.Services.AddAuthorization(op => 
{
    op.AddPolicy("Users", policy => policy.RequireRole("Users"));
    op.AddPolicy("Creators", policy => policy.RequireRole("Creators"));
    op.AddPolicy("Manager_Users", policy => policy.RequireRole("MUsers"));
    op.AddPolicy("Manager_Posts", policy => policy.RequireRole("MPosts"));
    op.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});
#endregion


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();



#region PostRouts Definition

app.MapGet("/AllPosts", async (Context db) => 
    await db.Posts.AsNoTracking().ToListAsync()).AllowAnonymous();

app.MapGet("/AllPosts/{tag}", async (string tag,Context db) =>
    await db.Posts.Where(t => t.Tag.ToLower().Contains(tag)).AsNoTracking().ToListAsync()).AllowAnonymous();

app.MapGet("/Post/{id}", async (int id, Context db) =>
{
    var post = await db.Posts.FindAsync(id);
    if(post == null)
        return Results.NotFound();

    var user = await db.Users.FindAsync(post.UserId);
    if(user == null)
        return Results.NoContent();
    
    return Results.Ok(
        new {
            Title = post.Title,
            Tag = post.Tag,
            Content = post.Content,
            DateCreate = post.DateCreate,

            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        }
    );
}
    ).AllowAnonymous();

app.MapPost("/CreatePost", async (Post post, Context db) =>
{
    post.DateCreate = DateTime.Now;
    post.LastModification = DateTime.Now;
    db.Posts.Add(post);
    await db.SaveChangesAsync();

    return Results.Created($"/Post/{post.Id}", post);
}).RequireAuthorization();

app.MapPut("/ModifiedPost/{id}", async (int id, Post inputpost, Context db) =>
{
    var post = await db.Posts.FindAsync(id);

    if (post is null) return Results.NotFound();

    post.Title = inputpost.Title;
    post.Tag = inputpost.Tag;
    post.Content = inputpost.Content;
    post.LastModification = DateTime.Now;

    await db.SaveChangesAsync();

    return Results.Created($"/Post/{inputpost.Id}", inputpost);
}).RequireAuthorization("Creators","Manager_Posts","Admin");

app.MapDelete("/DeletePost/{id}", async (int id, Context db) =>
{
    if (await db.Posts.FindAsync(id) is Post post)
    {
        db.Posts.Remove(post);
        await db.SaveChangesAsync();
        return Results.Ok(post);
    }

    return Results.NotFound();
}).RequireAuthorization("Creators","Manager_Posts","Admin");

#endregion

#region UserRouts Definition

app.MapGet("/AllUsers", async (Context db) => await db.Users.Select(blog => new {
        FirstName = blog.FirstName,
        LastName = blog.LastName,
        Email = blog.Email,
        Role = blog.Role
    }).AsNoTracking().ToListAsync()).RequireAuthorization();

app.MapGet("/AllUsersFirst/{Name}", async (string Name,Context db) => 
    await db.Users.Where(t => t.FirstName == Name).Select(blog => new {
        FirstName = blog.FirstName,
        LastName = blog.LastName,
        Email = blog.Email,
        Role = blog.Role
    }).ToListAsync()).RequireAuthorization();
    

app.MapGet("/AllUsersLast/{Name}", async (string Name,Context db) =>
    await db.Users.Where(t => t.LastName == Name).Select(blog => new {
        FirstName = blog.FirstName,
        LastName = blog.LastName,
        Email = blog.Email,
        Role = blog.Role
    }).ToListAsync()).RequireAuthorization();

app.MapGet("/UserEmail/{Email}", async (string Email,Context db) =>
    await db.Users.Where(t => t.Email == Email).Select(blog => new {
        FirstName = blog.FirstName,
        LastName = blog.LastName,
        Email = blog.Email,
        Role = blog.Role
    }).ToListAsync()).RequireAuthorization();

app.MapGet("/User/{id}", async (int id, Context db) =>
{
    var exists = await db.Users.FindAsync(id);
    if(exists == null)
        return Results.NotFound();

    return Results.Ok(
        new {
            FirstName = exists.FirstName,
            LastName = exists.LastName,
            Email = exists.Email,
            Role = exists.Role
        });
}).RequireAuthorization();

app.MapPost("/CreateUser", async (User user, Context db) =>
{
    user.DateCreate = DateTime.Now;
    user.LastModified = DateTime.Now;
    //user.Role = "Users";
    db.Users.Add(user);
    await db.SaveChangesAsync();

    var TokenService = new TokenServices();
    var token = TokenService.GenerateToken(user);
    user.Password = "";

    return Results.Ok(
        new {
            user = user,
            token = token
        });
}).AllowAnonymous();

app.MapPost("/Login", async(User user, Context db) =>
{
    var exists = await db.Users.FindAsync(user.Id);
    if(exists == null)
        return Results.NotFound();

    var TokenService = new TokenServices();

    var token = TokenService.GenerateToken(user);

    user.Password = "";

    return Results.Ok(
        new {
            user = user,
            token = token
        });
}).AllowAnonymous();

app.MapPut("/ModifiedUsers/{id}", async (int id, User inputUser, Context db) =>
{
    var user = await db.Users.FindAsync(id);

    if (user is null) return Results.NotFound();

    user.FirstName = inputUser.FirstName;
    user.LastName = inputUser.LastName;
    user.Email = inputUser.Email;
    user.Password = inputUser.Password;
    user.LastModified = DateTime.Now;
    user.Role = inputUser.Role;

    await db.SaveChangesAsync();

    return Results.NoContent();
}).RequireAuthorization();

app.MapDelete("/DeleteUser/{id}", async (int id, Context db) =>
{
    if (await db.Users.FindAsync(id) is User user)
    {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Results.Ok(user);
    }

    return Results.NotFound();
}).RequireAuthorization();

#endregion

app.Run();
