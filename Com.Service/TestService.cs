using Com.IService;
using Com.Models;
using Com.Models.Entity;
using Common.Interface;
using Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Service
{
    public class TestService : BaseService, ITestService
    {
        public TestService(IRepositoryFactory repositoryFactory, DataContext mydbcontext) : base(repositoryFactory, mydbcontext){}

        public List<w_users> GetUsers()
        {
            var service = CreateService<w_users>();
            return service.GetAll().ToList();
        }
    }
}
