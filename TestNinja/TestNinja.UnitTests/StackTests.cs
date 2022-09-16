using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests;

[TestFixture]
public class StackTests
{
    private Stack<string> _stack = null!;

    [SetUp]
    public void SetUp()
    {
        _stack = new();
    }

    [Test]
    public void Count_EmptyStack_ReturnsZero()
    {
        Assert.AreEqual(0, _stack.Count);
    }

    [Test]
    public void Push_ArgumentIsNull_ThrowsArgumentNullException()
    {
        Assert.That(() => _stack.Push(null!), Throws.ArgumentNullException);
    }

    [Test]
    public void Push_ValidArgument_AddsTheObjectToTheStack()
    {
        _stack.Push("a");

        Assert.That(_stack.Count, Is.EqualTo(1));
    }

    [Test]
    public void Pop_EmptyStack_ThrowsInvalidOperationException()
    {
        Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_StackWithObjects_ReturnsObjectOnTop()
    {
        _stack.Push("a");
        _stack.Push("b");
        _stack.Push("c");

        var result = _stack.Pop();

        Assert.That(result, Is.EqualTo("c"));
    }

    [Test]
    public void Pop_StackWithObjects_RemovesObjectOnTop()
    {
        _stack.Push("a");
        _stack.Push("b");
        _stack.Push("c");

        _stack.Pop();

        Assert.That(_stack.Count, Is.EqualTo(2));
    }

    [Test]
    public void Peek_EmptyStack_ThrowsInvalidOperationException()
    {
        Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
    }

    [Test]
    public void Peek_StackWithObjects_ReturnsCorrectItem()
    {
        _stack.Push("a");

        var item = _stack.Peek();

        Assert.That(item, Is.EqualTo("a"));
    }

    [Test]
    public void Peek_StackWithObjects_DoesNotRemoveTheObjectOnTop()
    {
        _stack.Push("a");
        _stack.Push("b");
        _stack.Push("c");

        _stack.Peek();

        Assert.That(_stack.Count, Is.EqualTo(3));
    }
}
