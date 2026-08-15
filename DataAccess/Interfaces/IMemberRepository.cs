using LibraryManagementSystem.DataAccess.Models;

namespace LibraryManagementSystem.DataAccess.Interfaces
{
    internal interface IMemberRepository
    {
        void AddMember(Member member);
        List<Member> GetAllMembers();
        Member GetMember(int memberId);
        void UpdateMember(Member member);
        void DeleteMember(int memberId);
    }
}
