using Alba.CsConsoleFormat;

using CLI.DTO;

namespace CLI.ConsoleInterface;

public class ConsoleWritterImpl : IConsoleWritter
{
    public async Task WriteTableAsync(IEnumerable<EntityData> data, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var document = CreateDocument(data);
        await Console.Out.WriteLineAsync(@"");
        ConsoleRenderer.RenderDocument(document);
        await Console.Out.WriteLineAsync(@"");
    }

    public async Task WriteStringAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Console.Out.WriteLineAsync(str);
    }

    private static Document CreateDocument(IEnumerable<EntityData> data)
    {
        var thickness = new LineThickness(LineWidth.Double, LineWidth.Single);

        return new(
            new Grid()
            {
                Color = ConsoleColor.Gray,
                Columns = {GridLength.Auto, GridLength.Star(1), GridLength.Auto},
                Children =
                {
                    new Cell(@"Setting Type"){Stroke = thickness},
                    new Cell(@"Key"){Stroke = thickness, Color = ConsoleColor.Yellow},
                    new Cell(@"Description"){Stroke = thickness},
                    new Cell(@"Current Setting"){Stroke = thickness},
                    new Cell(@"Is Supported"){Stroke = thickness},

                    data.Select(item => new[]
                    {
                        new Cell(item.SectionName),
                        new Cell(item.SettingId),
                        new Cell(item.CommonName),
                        new Cell(item.SettingValue),
                        new Cell(item.IsSupported.ToString())
                    })
                }
            }
            );
    }

    public async Task WriteExceptionAsync(Exception ex, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        try
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync(@$"ERROR: {ex.Message}");
        }
        finally
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
