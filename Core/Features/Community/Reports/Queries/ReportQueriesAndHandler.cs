using Core.Bases;
using MediatR;
using Service.Abstracts;

namespace Core.Features.Community.Reports.Queries.Models
{
    public class GetOpenReportsQuery : IRequest<Response<List<Core.Features.Community.Reports.Queries.Results.ReportListItem>>> { }
}

namespace Core.Features.Community.Reports.Queries.Results
{
    public class ReportListItem
    {
        public int Id { get; set; }
        public string TargetType { get; set; }
        public int TargetId { get; set; }
        public string Reason { get; set; }
        public string ReportedBy { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
    }
}

namespace Core.Features.Community.Reports.Queries.Handlers
{
    using Core.Features.Community.Reports.Queries.Models;
    using Core.Features.Community.Reports.Queries.Results;

    public class ReportQueryHandler : IRequestHandler<GetOpenReportsQuery, Response<List<ReportListItem>>>
    {
        private readonly ICommunityReportService _reportService;
        private readonly ResponseHandler _responseHandler;

        public ReportQueryHandler(ICommunityReportService reportService, ResponseHandler responseHandler)
        {
            _reportService = reportService;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<ReportListItem>>> Handle(GetOpenReportsQuery request, CancellationToken cancellationToken)
        {
            var reports = await _reportService.GetOpenReportsAsync();

            var result = reports.Select(r => new ReportListItem
            {
                Id = r.Id,
                TargetType = r.TargetType.ToString(),
                TargetId = r.PostId ?? r.CommentId ?? 0,
                Reason = r.Reason,
                ReportedBy = r.ReportedByUser?.UserName ?? "Unknown",
                Status = r.Status.ToString(),
                CreatedAt = r.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
            }).ToList();

            return _responseHandler.Success(result);
        }
    }
}
