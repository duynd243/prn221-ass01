using BusinessObject;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly MemberDAO _memberDao;

        public MemberRepository(MemberDAO memberDao)
        {
            _memberDao = memberDao;
        }

        

        public IEnumerable<Member> GetMembers()
        {
            return _memberDao.GetMembers();
        }
        public Member? Login(string email, string password)
        {
            return _memberDao.Login(email, password);
        }

        public Member GetMember(string email)
        {
            throw new NotImplementedException();
        }

        public Member GetMember(int memberId)
        {
            throw new NotImplementedException();
        }

        public void AddMember(Member member)
        {
            _memberDao.AddMember(member);
        }

        public void UpdateMember(Member member)
        {
            _memberDao.UpdateMember(member);
        }

        public void DeleteMember(int memberId)
        {
            _memberDao.DeleteMember(memberId);
        }
        
        public void RestoreMember(int memberId)
        {
            _memberDao.RestoreMember(memberId);
        }
    }
}