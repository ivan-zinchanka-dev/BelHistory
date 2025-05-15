using BelHistory.Domain.API.Models;

namespace BelHistory.Web.ViewModels.Home;

public class ExploreViewModel
{
    public HistoricalDocumentPath Path { get; private set; }
    public IReadOnlyList<HistoricalDocument> Documents { get; private set; }

    public ExploreViewModel(HistoricalDocumentPath path, IReadOnlyList<HistoricalDocument> documents)
    {
        Path = path;
        Documents = documents;
    }
}