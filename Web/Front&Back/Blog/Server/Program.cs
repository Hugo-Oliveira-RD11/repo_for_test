var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
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

builder.Services.AddAuthorization(op => 
{
    op.AddPolicy("Users", policy => policy.RequireRole("Users"));
    op.AddPolicy("Creators", policy => policy.RequireRole("Creators"));
    op.AddPolicy("Manager_Users", policy => policy.RequireRole("MUsers"));
    op.AddPolicy("Manager_Posts", policy => policy.RequireRole("MPosts"));
    op.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});


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

//Posts
app.MapGet("/AllPosts", async (Context db) => 
    await db.Posts.ToListAsync()).AllowAnonymous();

app.MapGet("/AllPosts/{tag}", async (string tag,Context db) =>
    await db.Posts.Where(t => t.Tag == tag).ToListAsync()).AllowAnonymous();

app.MapGet("/Post/{id}", async (int id, Context db) =>
    await db.Posts.FindAsync(id)
        is Post post
            ? Results.Ok(post)
            : Results.NotFound()).AllowAnonymous();

app.MapPost("/CreatePost", async (Post post, Context db) =>
{
    post.DateCreate = DateTime.Now;
    post.LastModification = DateTime.Now;
    db.Posts.Add(post);
    await db.SaveChangesAsync();

    return Results.Created($"/Post/{post.Id}", post);
}).RequireAuthorization("Creators","Manager_Posts","Admin");

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


//Users
//app.MapGet("/AllUsers", async (Context db) =>
//{
//    db.Users. 
//});

app.MapGet("/AllUsersFirst/{Name}", async (string Name,Context db) =>
    await db.Users.Where(t => t.FirstName == Name).Select(t => t.FirstName).ToListAsync());

app.MapGet("/AllUsersLast/{Name}", async (string Name,Context db) =>
    await db.Users.Where(t => t.LastName == Name).ToListAsync());

app.MapGet("/UserEmail/{Email}", async (string Email,Context db) =>
    await db.Users.Where(t => t.Email == Email).ToListAsync()).RequireAuthorization("Manager_Users","Admin");

app.MapGet("/User/{id}", async (int id, Context db) =>
    await db.Users.FindAsync(id)
        is User user
            ? Results.Ok(user)
            : Results.NotFound());

app.MapPost("/CreateUser", async (User user, Context db) =>
{
    user.DateCreate = DateTime.Now;
    user.LastModified = DateTime.Now;
    user.Post=null;
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

app.MapPost("/Login", async(User user, Context db,IConfiguration _config) =>
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

app.Run();
