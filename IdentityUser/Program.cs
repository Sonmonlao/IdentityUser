var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

var users = new List<Registration>();


app.MapPost("/registration", (Registration body) => {  
    users.Add(body);
    return("ok");
});
app.MapPost("/login", (Login body) => {
    var user = users.Find(a => a.login == body.login);
    if (user == null) return "Пользователь не найден";
    if (user.password == body.password) return "Вход выполнен";
    return "Неверный пароль";
});
app.MapPost("/role", (ChangeRole body) => {
    var user = users.Find(a => a.login == body.login);
    if (user == null) return "Пользователь не найден";
    user.role = body.role;
    return user.role.ToString;
});
app.Run();

class User 
{
    public string login { get; set; }
    public string password { get; set; }
    public Roles role {get;set;}
    public enum Roles
    {
        Admin,
        Customer,
        Performer
    }
}
class Registration
{
    public string login { get; set; }
    public string password { get; set; }
}

class Login
{
    public string login { get; set; } 
    public string password { get; set; }
}

class ChangeRole 
{
    public string login { get; set; }
    public User.Roles role { get; set; }
}