namespace IcerDesign.CCHelper
{
    internal class WorkProgress
    {
        public WorkProgress(string detail, int progress = 0, int total = 0)
        {
            this.StepDetail = detail;
            this.Progress = progress;
            this.Total = total;
        }

        public int Progress { get; set; }
        public int Total { get; set; }
        public string StepDetail { get; set; }
    }
}