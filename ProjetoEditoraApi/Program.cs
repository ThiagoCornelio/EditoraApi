var builder = WebApplication.CreateBuilder(args);
AppExtensions.ConfigureAuthentication(builder);
AppExtensions.ConfigureMvc(builder);
AppExtensions.ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer(); //Adiciona o Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

AppExtensions.LoadConfiguration(app);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); 

app.UseStaticFiles();
app.UseResponseCompression();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
