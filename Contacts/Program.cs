using System.Text.Json;
using Contacts.Model;

/*
 * 1: Backend med hardkodete data
 * 2: "In-memory db" -> List<Person>
 *    - Slette
 *    - Endre
 *    - Legge til ny
 *    - Lese
 *   Hva er begrensningen med dette?
 * 3: Fil-basert backend
 */

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapGet("/people", () =>
{
    var files = Directory.GetFiles("people");
    var people = new List<Person>();
    foreach (var file in files)
    {
        var json = File.ReadAllText(file);
        var person = JsonSerializer.Deserialize<Person>(json);
        people.Add(person);
    }
    return people;
});
app.MapDelete("/people/{id}", (Guid id) =>
{
    File.Delete("people/" + id + ".json");
});
app.MapPost("/people", (Person person) =>
{
    var json = JsonSerializer.Serialize(person);
    File.WriteAllText("people/" + person.Id.ToString() + ".json", json);
});
app.MapPut("/people", (Person person) =>
{
    var json = JsonSerializer.Serialize(person);
    File.WriteAllText("people/" + person.Id.ToString() + ".json", json);
});
app.Run();

/*
    var createOrUpdate = (Person person) =>
    {
        var json = JsonSerializer.Serialize(person);
        File.WriteAllText("people/" + person.Id.ToString() + ".json", json);
    };
    app.MapPost("/people", createOrUpdate);
    app.MapPut("/people", createOrUpdate); 
 */


/*
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.Build();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    var people = new List<Person>{
        new Person ("Per", "per@mail.com"),
        new Person ("Pål", "pål@mail.com"),
        new Person ("Espen", "espen@mail.com"),
        new Person ("Terje", "terje@mail.com"),
    };

    app.MapGet("/people", () =>
    {
        return people;
    });
    app.MapDelete("/people/{id}", (Guid id) =>
    {
        people = people.Where(p => p.Id != id).ToList();
    });
    app.MapPost("/people", (Person person) =>
    {
        people.Add(person);
    });
    app.MapPut("/people", (Person person) =>
    {
        var thePerson = people.SingleOrDefault(p => p.Id == person.Id);
        thePerson.FirstName = person.FirstName;
        thePerson.Email = person.Email;
    });
    app.Run();
*/ 