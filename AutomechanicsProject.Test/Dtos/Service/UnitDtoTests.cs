using Xunit;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class UnitDtoTests
    {
        [Fact]
        public void CanCreateUnitDto()
        {
            var dto = new UnitDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var id = Guid.NewGuid();
            var dto = new UnitDto
            {
                Id = id,
                Name = "Литр",
                ShortName = "л"
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal("Литр", dto.Name);
            Assert.Equal("л", dto.ShortName);
        }

        [Fact]
        public void ShortName_CanBeNullOrEmpty()
        {
            var dto = new UnitDto { ShortName = null };
            Assert.Null(dto.ShortName);

            dto.ShortName = "";
            Assert.Equal("", dto.ShortName);
        }
    }
}