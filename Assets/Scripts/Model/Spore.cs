public class Spore : RedBloodCell
{
    public override string GetTypeName()
    {
        return "Spore";
    }

    public override void doAction(Unit unit)
    {
        if (unit.GetType() == typeof(Infection))
        {
            commandQueue.Clear();
            commandQueue.Enqueue(new ColonizeCommand((Infection)unit));
        }
    }
}
