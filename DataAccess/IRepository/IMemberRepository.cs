using BusinessObject;

namespace DataAccess.IRepository
{
    public interface IMemberRepository
    {
        Member? Login(string email, string password);
        Member GetMember(string email);
        Member GetMember(int memberId);
        void AddMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(int memberId);
        void RestoreMember(int memberId);
        IEnumerable<Member> GetMembers();
    }
}