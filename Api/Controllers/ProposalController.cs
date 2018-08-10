using Logic.Interfaces;
using Models.DTO;
using System; 
using System.Linq;
using System.Web.Http;
using System.Linq.Expressions;
using Models.Сonverters;
using Common.Extentions;

namespace Api.Controllers
{
    public class ProposalController : BaseController
    {
        private readonly IProposalManager _proposalManager;
        public ProposalController(IProposalManager proposalManager)
        { 
            _proposalManager = proposalManager;
        }

        public IHttpActionResult Get([FromUri]ProposalRequest proposalRequest)
        {
            var isLogin = Login(proposalRequest.Token);

            if (!isLogin)
            {
                return Content(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
            }

            if(ModelState.IsValid)
            {
                var hash = proposalRequest.GetHashCode().ToString();
                var obj = memoryCache.Get(hash);

                if (obj != null)
                {
                    return Json((ProposalResponse)obj);
                }

                var filter = Filter(proposalRequest);
                return Json(_proposalManager.Read(filter).Skip(proposalRequest.Skip).Take(proposalRequest.Take).ToList());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
         
        public IHttpActionResult Post([FromBody]ProposalRequest proposalRequest)
        {
            var isLogin = Login(proposalRequest.Token);

            if (!isLogin)
            {
                return Content(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
            }

            if (ModelState.IsValid)
            {
                _proposalManager.Update(proposalRequest.Convert());
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            } 
        }

        public IHttpActionResult Put([FromBody]ProposalRequest proposalRequest)
        {
            var isLogin = Login(proposalRequest.Token);

            if (!isLogin)
            {
                return Content(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
            }


            if (ModelState.IsValid)
            {
                _proposalManager.Add(proposalRequest.Convert());
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            } 
        } 

        public IHttpActionResult Delete([FromBody]ProposalRequest proposalRequest)
        {
            var isLogin = Login(proposalRequest.Token);

            if (!isLogin)
            {
                return Content(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
            } 

            if (ModelState.IsValid)
            { 
                _proposalManager.Delete(proposalRequest.Convert());
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            } 
        }

        #region Filter
        private Expression<Func<Proposal, bool>> Filter(ProposalRequest model)
        {
            var filter = PredicateBuilder.True<Proposal>();
            if (!string.IsNullOrEmpty(model.Name))
                filter = filter.AndAlso(x => x.Name.Contains(model.Name));

            return filter;
        }
        #endregion
    }
}