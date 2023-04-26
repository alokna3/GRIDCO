using GRIDCO.Core.ProjectAggregate;
using System.Collections.Generic;

namespace GRIDCO.Web.Endpoints.ProjectEndpoints
{
    public class ProjectListResponse
    {
        public List<ProjectRecord> Projects { get; set; } = new();
    }
}
