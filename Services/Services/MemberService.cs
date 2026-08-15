using LibraryManagementSystem.Services.Interfaces;
using LibraryManagementSystem.DataAccess.Models;
using LibraryManagementSystem.DataAccess.Interfaces;
using System.Collections.Generic;

namespace LibraryManagementSystem.Services.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public void AddMember(Member member)
        {
            _memberRepository.AddMember(member);
        }

        public Member GetMember(int memberId)
        {
            return _memberRepository.GetMember(memberId);
        }

        public List<Member> GetAllMembers()
        {
            return _memberRepository.GetAllMembers();
        }

        public void DeleteMember(int memberId)
        {
            _memberRepository.DeleteMember(memberId);
        }

        public void UpdateMember(Member member)
        {
            _memberRepository.UpdateMember(member);
        }
    }
}
