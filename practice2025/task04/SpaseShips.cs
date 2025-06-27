public interface ISpaceship
{
    void MoveForward();
    void Rotate(int angle);
    void Fire();
    int Speed { get; }
    int Position { get; }
    int CurrentAngle { get; }
    int FirePower { get; }
    int BulletSpeed { get; }
    int HealtsEnemy { get; }
}

public class Fighter : ISpaceship
{
    public int Speed { get; } = 100;
    public int Position { get; private set; } = 42;
    public int CurrentAngle { get; private set; } = 67;
    public int FirePower { get; } = 50;
    public int BulletSpeed { get; } = 300;
    public int HealtsEnemy { get; private set; } = 1000;

    public void MoveForward()
    {
        Position += Speed;
    }

    public void Rotate(int angle)
    {
        CurrentAngle = (CurrentAngle + angle) % 360;
        if (CurrentAngle < 0)
        {
            CurrentAngle += 360;
        }
    }

    public void Fire()
    {
        Position += BulletSpeed;
        HealtsEnemy -= FirePower;
    }
}

public class Cruiser : ISpaceship
{
    public int Speed { get; } = 50;
    public int Position { get; private set; } = 52;
    public int CurrentAngle { get; private set; } = 87;
    public int FirePower { get; } = 100;
    public int BulletSpeed { get; } = 100;
    public int HealtsEnemy { get; private set; } = 1000;

    public void MoveForward()
    {
        Position += Speed;
    }

    public void Rotate(int angle)
    {
        CurrentAngle = (CurrentAngle + angle) % 360;
        if (CurrentAngle < 0)
        {
            CurrentAngle += 360;
        }
    }

    public void Fire()
    {
        Position += BulletSpeed;
        HealtsEnemy -= FirePower;
    }
}