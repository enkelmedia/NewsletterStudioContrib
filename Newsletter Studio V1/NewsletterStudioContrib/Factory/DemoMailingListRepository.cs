using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewsletterStudio.Core.Interfaces.Data;
using NewsletterStudio.Core.Model;
using NewsletterStudio.Infrastucture.Data;

namespace NewsletterStudioContrib.Factory
{
    public class DemoMailingListRepository : IMailingListRepository, IRepository<MailingList>
    {
        private static List<MailingList> _mailingLists;

        public DemoMailingListRepository()
        {
            if (_mailingLists == null)
            {
                _mailingLists = new List<MailingList>();
                _mailingLists.Add(new MailingList() { Id = 1, Name = "The first list" });
                _mailingLists.Add(new MailingList() { Id = 2, Name = "Another (2) list" });
                _mailingLists.Add(new MailingList() { Id = 3, Name = "List nr three" });
                _mailingLists.Add(new MailingList() { Id = 4, Name = "The last list" });
            }
        }

        public int Save(MailingList mailistList)
        {
            mailistList.Id = _mailingLists.Count;

            _mailingLists.Add(mailistList);
            return mailistList.Id;
        }

        public bool Remove(int mailingListId)
        {
            throw new NotImplementedException();
        }

        public MailingList GetById(int mailingListId)
        {
            return _mailingLists.First(x => x.Id == mailingListId);
        }

        public IList<MailingList> GetAll()
        {
            return _mailingLists;
        }
    }
    
}
