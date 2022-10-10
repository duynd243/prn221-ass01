using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        private readonly PRN221_Ass01_FStoreContext _context;

        public MemberDAO(PRN221_Ass01_FStoreContext context)
        {
            _context = context;
        }
        
        public List<Member> GetMembers()
        {
            return _context.Members.ToList();
        }

        public Member? Login(string email, string password)
        {
            return _context.Members
                .FirstOrDefault(m => m.Email == email
                                     && m.Password == password
                );
        }

        public void UpdateMember(Member member)
        {
            _context.Entry(member).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AddMember(Member member)
        {
            var dbMember = _context.Members.FirstOrDefault(m => m.Email == member.Email);
            if (dbMember is not null)
            {
                throw new Exception("Email already exists");
            }
            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public void DeleteMember(int id)
        {
            var member = _context.Members.Find(id);
            if (member is null) return;
            member.Status = false;
            _context.Entry(member).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RestoreMember(int id)
        {
            var member = _context.Members.Find(id);
            if (member is null) return;
            member.Status = true;
            _context.Entry(member).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}