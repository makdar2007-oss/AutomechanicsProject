using Xunit;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class RecipientDtoTests
    {
        [Fact]
        public void CanCreateRecipientDto()
        {
            var dto = new RecipientDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetIdAndCompanyName()
        {
            var id = Guid.NewGuid();
            var dto = new RecipientDto
            {
                Id = id,
                CompanyName = "ООО Ромашка"
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal("ООО Ромашка", dto.CompanyName);
        }

        [Fact]
        public void CompanyName_CanBeNullOrEmpty()
        {
            var dto = new RecipientDto { CompanyName = null };
            Assert.Null(dto.CompanyName);

            dto.CompanyName = "";
            Assert.Equal("", dto.CompanyName);
        }
    }
}