namespace MemoStack.Model;

public class MemoModel
{
    public MemoModel(string text, int depth)
    {
        Text = text;
        Depth = depth;
    }

    public long Id { get; init; }
    public string Text { get; set; }
    public int Depth { get; set; }
}