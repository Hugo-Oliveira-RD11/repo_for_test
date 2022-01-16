var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
var app = builder.Build();


//Posts
app.MapGet("/AllPosts", async (Context db) => 
    await db.Posts.ToListAsync());

app.MapGet("/AllPosts/{tag}", async (string tag,Context db) =>
    await db.Posts.Where(t => t.Tag == tag).ToListAsync());

app.MapGet("/Post/{id}", async (int id, Context db) =>
    await db.Posts.FindAsync(id)
        is Post post
            ? Results.Ok(post)
            : Results.NotFound());

app.MapPost("/CreatePost", async (Post post, Context db) =>
{
    post.DateCreate = DateTime.Now;
    post.LastModification = DateTime.Now;
    db.Posts.Add(post);
    await db.SaveChangesAsync();

    return Results.Created($"/Post/{post.Id}", post);
});

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
});

app.MapDelete("/DeletePost/{id}", async (int id, Context db) =>
{
    if (await db.Posts.FindAsync(id) is Post post)
    {
        db.Posts.Remove(post);
        await db.SaveChangesAsync();
        return Results.Ok(post);
    }

    return Results.NotFound();
});


//Users
app.MapGet("/AllUsers", async (Context db) =>
    await db.Users.ToListAsync());

app.MapGet("/AllUsersFirst/{Name}", async (string Name,Context db) =>
    await db.Users.Where(t => t.FirstName == Name).ToListAsync());

app.MapGet("/AllUsersLast/{Name}", async (string Name,Context db) =>
    await db.Users.Where(t => t.LastName == Name).ToListAsync());

app.MapGet("/UserEmail/{Name}", async (string Name,Context db) =>
    await db.Users.Where(t => t.Email == Name).ToListAsync());

app.MapGet("/User/{id}", async (int id, Context db) =>
    await db.Users.FindAsync(id)
        is User user
            ? Results.Ok(user)
            : Results.NotFound());

app.MapPost("/CreateUser", async (User user, Context db) =>
{
    user.DateCreate = DateTime.Now;
    user.LastModified = DateTime.Now;
    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created($"/Users/{user.Id}", user);
});

app.MapPut("/ModifiedUsers/{id}", async (int id, User inputUser, Context db) =>
{
    var user = await db.Users.FindAsync(id);

    if (user is null) return Results.NotFound();

    user.FirstName = inputUser.FirstName;
    user.LastName = inputUser.LastName;
    user.Email = inputUser.Email;
    user.Password = inputUser.Password;
    user.LastModified = DateTime.Now;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/DeleteUser/{id}", async (int id, Context db) =>
{
    if (await db.Users.FindAsync(id) is User user)
    {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Results.Ok(user);
    }

    return Results.NotFound();
});

app.Run();
