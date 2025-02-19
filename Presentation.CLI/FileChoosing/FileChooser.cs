using DataAccess.Abstract;

namespace Presentation.CLI.FileChoosing;
public class FileChooser(params IFileType[] fileTypes)
{
    private const string DefaultFileName = "transport";
    
    public ITransportRepository ChooseFile()
    {
        while (true)
        {
            Console.WriteLine("Выберите тип файла:");
            foreach (var (fileType, i) in fileTypes.Select((ft, i) => (ft, i)))
            {
                Console.WriteLine($"{i + 1}. {fileType.Title}");
            }

            Console.Write("> ");

            if (!int.TryParse(Console.ReadLine(), out int fileTypeNumber))
            {
                Console.WriteLine("Вы должны ввести число");
                continue;
            }
            
            var chosenFileType = fileTypes.Where((_, i) => i == fileTypeNumber - 1).FirstOrDefault();
            if (chosenFileType == null)
            {
                Console.WriteLine("Вы должны ввести номер одного из вариантов");
                continue;
            }
            
            Console.Write($"Введите название файла (по-умолчанию \"{DefaultFileName}\"): ");
            
            string? fileName = Console.ReadLine();
            if (string.IsNullOrEmpty(fileName?.Trim()))
            {
                fileName = DefaultFileName;
            }
            
            return fileTypes[fileTypeNumber - 1]
                .CreateTransportRepository($"{fileName}.{chosenFileType.Extension}");
        }
    }
}
