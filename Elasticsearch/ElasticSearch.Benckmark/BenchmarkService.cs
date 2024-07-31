using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace ElasticSearch.Benckmark;

[ShortRunJob, Config(typeof(Config))]
public class BenchmarkService
{
    private class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle(BenchmarkDotNet.Columns.RatioStyle.Trend);
        }
    }

    ElasticSearchService elasticSearchService = new();
    ApplicationDbContext context = new();

    [Benchmark(Baseline = true)]
    public void ElasticSearch()
    {
        elasticSearchService.GetAll("a");
    }

    [Benchmark]
    public void MSSQL()
    {
        //context.Products.Where(p => p.Description.ToLower().Contains("a")).ToList();
        context.Products.ToList();
    }
}
