using AuthService.Domain.SeedWork;

namespace AuthService.Test.Unit.Domain.SeedWork
{
    public class EntityBaseTest
    {
        private class TestEntity : EntityBase<Guid>
        {
            public TestEntity() : base(Guid.NewGuid())
            {
            }

            public TestEntity(Guid id) : base(id)
            {
            }
        }

        [Fact]
        public void Constructor_WhenEntityIsCreated_ShouldSetIdAndCreatedDate()
        {
            // Arrange
            Guid defaultGuid = default;
            DateTime defaultDateTime = default;

            // Act
            var entity = new TestEntity();

            // Assert
            Assert.NotEqual(defaultGuid, entity.Id);
            Assert.NotEqual(defaultDateTime, entity.CreatedDate);
        }

        [Fact]
        public void Equals_WhenTwoEntitiesHaveSameId_ShouldReturnTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity1 = new TestEntity(id);
            var entity2 = new TestEntity(id);

            // Act
            var result = entity1.Equals(entity2);
            var resultObj = entity1.Equals((object)entity2);

            // Assert
            Assert.True(result, "The equals method accepting the same type did not return true.");
            Assert.True(resultObj, "The equals method accepting an object type did not return true.");
        }

        [Fact]
        public void Equals_WhenTwoEntitiesHaveDifferentId_ShouldReturnFalse()
        {
            // Arrange
            var entity1 = new TestEntity(Guid.NewGuid());
            var entity2 = new TestEntity(Guid.NewGuid());

            // Act
            var result = entity1.Equals(entity2);
            var resultObj = entity1.Equals((object)entity2);

            // Assert
            Assert.False(result, "The equals method accepting the same type did not return false.");
            Assert.False(resultObj, "The equals method accepting an object type did not return false.");
        }

        [Fact]
        public void Equals_WhenOneEntityIsNull_ShouldReturnFalse()
        {
            // Arrange
            var entity = new TestEntity(Guid.NewGuid());

            // Act
            var result = entity.Equals(null);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var resultObj = entity.Equals((object)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            // Assert
            Assert.False(result, "The equals method accepting the same type did not return false.");
            Assert.False(resultObj, "The equals method accepting an object type did not return false.");
        }

        [Fact]
        public void EqualOperator_WhenTwoEntitiesHaveSameId_ShouldReturnTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity1 = new TestEntity(id);
            var entity2 = new TestEntity(id);

            // Act
            var result = entity1 == entity2;

            // Assert
            Assert.True(result, "The equals operator did not return true.");
        }

        [Fact]
        public void EqualOperator_WhenTwoEntitiesHaveDifferentId_ShouldReturnFalse()
        {
            // Arrange
            var entity1 = new TestEntity(Guid.NewGuid());
            var entity2 = new TestEntity(Guid.NewGuid());

            // Act
            var result = entity1 == entity2;

            // Assert
            Assert.False(result, "The equal operator did not return false.");
        }

        [Fact]
        public void EqualOperator_WhenOneEntityIsNull_ShouldReturnFalse()
        {
            // Arrange
            var entity = new TestEntity(Guid.NewGuid());

            // Act
            var result = entity == null;

            // Assert
            Assert.False(result, "The equal operator did not return false.");
        }

        [Fact]
        public void NotEqualOperator_WhenTwoEntitiesHaveSameId_ShouldReturnFalse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity1 = new TestEntity(id);
            var entity2 = new TestEntity(id);

            // Act
            var result = entity1 != entity2;

            // Assert
            Assert.False(result, "The equals operator did not return false.");
        }

        [Fact]
        public void NotEqualOperator_WhenTwoEntitiesHaveDifferentId_ShouldReturnTrue()
        {
            // Arrange
            var entity1 = new TestEntity(Guid.NewGuid());
            var entity2 = new TestEntity(Guid.NewGuid());

            // Act
            var result = entity1 != entity2;

            // Assert
            Assert.True(result, "The equal operator did not return true.");
        }

        [Fact]
        public void NotEqualOperator_WhenOneEntityIsNull_ShouldReturnTrue()
        {
            // Arrange
            var entity = new TestEntity(Guid.NewGuid());

            // Act
            var result = entity != null;

            // Assert
            Assert.True(result, "The equal operator did not return true.");
        }

        [Fact]
        public void GetHashCode_WhenIsCalled_ShouldReturnHashCodeOfId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new TestEntity(id);

            // Act
            var hashCode = entity.GetHashCode();

            // Assert
            Assert.Equal(id.GetHashCode(), hashCode);
        }
    }
}
