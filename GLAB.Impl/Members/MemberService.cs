using GLAB.APP.Members;
using GLAB.Domains.Models.Members;
using GLAB.Domains.Models.Users;
using GLAB.Infra.Storages.MembersStorages;

namespace GLAB.Impl.Members;

public class MemberService:IMemberService
{
    private readonly IMemberStorage _memberStorage;
    
    public MemberService(IMemberStorage storage)
    {
        this._memberStorage = storage;
    }
    
    public async Task<Member> GetMemberByUserId(string UserId)
    {
        var fetchedMember = await _memberStorage.SelectMemberByUserId(UserId);
        return fetchedMember;
    }
}


