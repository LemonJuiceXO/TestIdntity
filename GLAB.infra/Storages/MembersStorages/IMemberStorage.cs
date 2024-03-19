using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLAB.Domains.Models.Members;

namespace GLAB.Infra.Storages.MembersStorages
{
    public  interface IMemberStorage
    {
        Task<List<Member>> SelectMembers();

        Task InsertMember(Member member);

        Task UpdateMember(Member member);

        Task DeleteMember(string id);

        Task<Member> SelectMemberByUserId(string UserId);




    }
}
