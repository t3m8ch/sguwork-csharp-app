using BusinessLogic.Impl;
using Presentation.CLI.Commands;
using Presentation.CLI.FileChoosing;

var fileChooser = new FileChooser(new TextFileType(), new JsonFileType());
var repo = fileChooser.ChooseFile();
var service = new TransportService(repo);

var app = new CommandDispatcher(
    new CreateCommand(service), 
    new GetAllCommand(service),
    new GetWithCapacityCommand(service),
    new RemoveCommand(service),
    new SaveCommand(service));
    
app.RunLoop();
