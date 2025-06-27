using Xunit;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(52, cruiser.Position);
        Assert.Equal(87, cruiser.CurrentAngle);
        Assert.Equal(100, cruiser.FirePower);
        Assert.Equal(100, cruiser.BulletSpeed);
        Assert.Equal(1000, cruiser.HealtsEnemy);
    }

    [Fact]    
    public void Fighter_ShouldHaveCorrectStats()
    {
        ISpaceship fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(42, fighter.Position);
        Assert.Equal(67, fighter.CurrentAngle);
        Assert.Equal(50, fighter.FirePower);
        Assert.Equal(300, fighter.BulletSpeed);
        Assert.Equal(1000, fighter.HealtsEnemy);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Cruiser_MoveForvard()
    {
        var cruiser = new Cruiser();
        cruiser.MoveForward();
        Assert.Equal(102, cruiser.Position);
    }

    [Fact]
    public void Fighter_MoveForvard()
    {
        var fighter = new Fighter();
        fighter.MoveForward();
        Assert.Equal(142, fighter.Position);
    }

    [Fact]
    public void Cruiser_Rotate()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(132);
        Assert.Equal(219, cruiser.CurrentAngle);
    }

    [Fact]
    public void Fighter_Rotate()
    {
        var fighter = new Fighter();
        fighter.Rotate(-82);
        Assert.Equal(345, fighter.CurrentAngle);
    }

    [Fact]
    public void Cruiser_Fire()
    {
        var cruiser = new Cruiser();
        cruiser.Fire();
        Assert.Equal(152, cruiser.Position);
        Assert.Equal(900, cruiser.HealtsEnemy);
    }

    [Fact]
    public void Fighter_Fire()
    {
        var fighter = new Fighter();
        fighter.Fire();
        Assert.Equal(342, fighter.Position);
        Assert.Equal(950, fighter.HealtsEnemy);
    }
}