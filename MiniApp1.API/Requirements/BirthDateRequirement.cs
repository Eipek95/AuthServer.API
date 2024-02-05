using Microsoft.AspNetCore.Authorization;

namespace MiniApp1.API.Requirements
{
    //policy tabanlı class
    public class BirthDateRequirement : IAuthorizationRequirement
    {
        public int Age { get; set; }//program.cs içinde verilir

        public BirthDateRequirement(int age)
        {
            Age = age;
        }
    }

    public class BirthTimeRequirementHandler : AuthorizationHandler<BirthDateRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BirthDateRequirement requirement)
        {
            var birthDate = context.User.FindFirst("birth-date");//payloaddan değer alma
            if (birthDate == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var today = DateTime.Now;
            var age = today.Year - Convert.ToDateTime(birthDate.Value).Year;
            if (requirement.Age <= age)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
