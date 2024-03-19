using GLAB.Domains.Models.Members;

namespace GLAB.APP.Members;

public interface IMemberService
{
    Task<Member> GetMemberByUserId(string UserId);
}