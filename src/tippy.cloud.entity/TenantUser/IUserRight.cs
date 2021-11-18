namespace tippy.cloud.entity
{ 
    public interface IUserRight
    {
        int deptid { get; set; }
        int postionid { get; set; }
        int userid { get; set; }
        string username { get; set; }
        string userpico { get; set; }
        string usermobile { get; set; }
        int tenantId { get; set; }
      
    }
}