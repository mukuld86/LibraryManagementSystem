using LibraryManagementSystem.DataAccess.Models;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IMemberService
    {
        void AddMember(Member member);
        Member GetMember(int memberId);
        List<Member> GetAllMembers();
        void DeleteMember(int memberId);
        void UpdateMember(Member member);
    }
}
