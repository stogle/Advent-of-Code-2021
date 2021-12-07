using System.Text;

namespace AdventOfCode2021.Amphipod;

public class Amphipod
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var amphipod = new Amphipod(reader);
        int result = amphipod.GetMinimumEnergy();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public Amphipod(TextReader reader)
    {
        _reader = reader;
    }

    public int GetMinimumEnergy()
    {
        _ = _reader.ReadLine();             // #############
        _ = _reader.ReadLine();             // #...........#
        string line1 = _reader.ReadLine()!; // ###X#X#X#X###
        string line2 = _reader.ReadLine()!; //   #X#X#X#X#
        _ = _reader.ReadLine();             //   #########

        var state = State.Create(line1, line2);
        int energy = 0;
        var unvisited = new PriorityQueue<State, int>();
        unvisited.Enqueue(state, energy);

        while (unvisited.TryPeek(out _, out energy))
        {
            // Put all states of equal energy into a HashSet to remove duplicates
            HashSet<State> statesOfEqualEnergy = new HashSet<State>();
            do
            {
                statesOfEqualEnergy.Add(unvisited.Dequeue());
            }
            while (unvisited.TryPeek(out _, out int nextEnergy) && nextEnergy == energy);

            foreach (State nextState in statesOfEqualEnergy)
            {
                if (nextState.IsOrganized())
                {
                    return energy;
                }

                nextState.EnqueueNextStates(unvisited, energy);
            }
        }

        return int.MaxValue;
    }

    public class State
    {
        public static State Create(string line1, string line2)
        {
            Space[] spaces =
            {
                new Hallway(),
                new Hallway(),
                new SideRoom(Content.A, 2, Enum.Parse<Content>(line2.Substring(3, 1)), Enum.Parse<Content>(line1.Substring(3, 1))),
                new Hallway(),
                new SideRoom(Content.B, 2, Enum.Parse<Content>(line2.Substring(5, 1)), Enum.Parse<Content>(line1.Substring(5, 1))),
                new Hallway(),
                new SideRoom(Content.C, 2, Enum.Parse<Content>(line2.Substring(7, 1)), Enum.Parse<Content>(line1.Substring(7, 1))),
                new Hallway(),
                new SideRoom(Content.D, 2, Enum.Parse<Content>(line2.Substring(9, 1)), Enum.Parse<Content>(line1.Substring(9, 1))),
                new Hallway(),
                new Hallway()
            };

            return new State(spaces);
        }

        private static readonly IDictionary<Content, int> EnergyPerStep = new Dictionary<Content, int>
        {
            [Content.A] = 1,
            [Content.B] = 10,
            [Content.C] = 100,
            [Content.D] = 1000,
        };

        //            1
        //  01 3 5 7 90  Hallways
        // #############
        // #...........#
        // ###X#X#X#X###
        //   #X#X#X#X#
        //   #########
        //    2 4 6 8    Side rooms
        private readonly Space[] _spaces;

        public State(Space[] spaces)
        {
            _spaces = spaces;
        }

        public bool IsOrganized()
        {
            return _spaces.All(s => s.IsOrganized());
        }

        public void EnqueueNextStates(PriorityQueue<State, int> unvisited, int energy)
        {
            for (int i = 0; i < _spaces.Length; i++)
            {
                Space from = _spaces[i];
                if (from.CanPop(out Content content))
                {
                    for (int j = 0; j < _spaces.Length; j++)
                    {
                        Space to = _spaces[j];
                        if (to.CanPush(from, content))
                        {
                            if (Enumerable.Range(Math.Min(i, j) + 1, Math.Abs(j - i) - 1)
                                    .Select(k => _spaces[k])
                                    .All(s => !s.IsBlocking()))
                            {
                                (State newState, int newEnergy) = Move(i, j, energy);
                                unvisited.Enqueue(newState, newEnergy);
                            }
                        }
                    }
                }
            }
        }

        private (State state, int Energy) Move(int i, int j, int energy)
        {
            Space[] spaces = _spaces.ToArray();
            spaces[i] = spaces[i].Pop(out Content content, out int popDistance);
            spaces[j] = spaces[j].Push(content, out int pushDistance);
            int distance = popDistance + Math.Abs(i - j) + pushDistance;
            return (new State(spaces), energy + distance * EnergyPerStep[content]);
        }

        public override bool Equals(object? obj) => obj is State state && _spaces.SequenceEqual(state._spaces);

        public override int GetHashCode()
        {
            var result = new HashCode();
            foreach (var space in _spaces)
            {
                result.Add(space);
            }
            return result.ToHashCode();
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append('#', _spaces.Length + 2).AppendLine();
            result.Append('#').AppendJoin(string.Empty, _spaces.Select(s => s.IsHallway() ? s.ToString() : ".")).AppendLine("#");
            result.Append('#').AppendJoin(string.Empty, _spaces.Select(s => !s.IsHallway() ? s.ToString()[1] : '#')).AppendLine("#");
            result.Append("  ").AppendJoin(string.Empty, _spaces.Skip(1).Take(_spaces.Length - 2).Select(s => !s.IsHallway() ? s.ToString()[0] : '#')).AppendLine();
            result.Append("  ").Append('#', _spaces.Length - 2);
            return result.ToString();
        }
    }

    public abstract class Space
    {
        protected int Capacity { get; }
        protected IReadOnlyList<Content> Contents { get; }

        protected Space(int capacity, params Content[] contents)
        {
            Capacity = capacity;
            Contents = contents;
        }

        public virtual bool IsOrganized() => !Contents.Any();

        public virtual bool IsHallway() => true;

        public bool IsBlocking() => IsHallway() && Contents.Any();

        public virtual bool CanPop(out Content content)
        {
            content = Contents.LastOrDefault();
            return Contents.Any();
        }

        public virtual bool CanPush(Space from, Content content) => Contents.Count < Capacity;

        public abstract Space Pop(out Content content, out int distance);

        public abstract Space Push(Content content, out int distance);

        public override bool Equals(object? obj) => obj is Space space && Capacity == space.Capacity && Contents.SequenceEqual(space.Contents);

        public override int GetHashCode()
        {
            var result = new HashCode();
            result.Add(Capacity);
            foreach (var content in Contents)
            {
                result.Add(content);
            }
            return result.ToHashCode();
        }

        public override string ToString() => string.Join(string.Empty, Contents).PadRight(Capacity, '.');
    }

    public class SideRoom : Space
    {
        private readonly Content _expectedContent;

        public SideRoom(Content expectedContent, int capacity, params Content[] contents) : base(capacity, contents)
        {
            _expectedContent = expectedContent;
        }

        public override bool IsOrganized() => Contents.Count == Capacity && Contents.All(c => c == _expectedContent);

        public override bool IsHallway() => false;

        public override bool CanPop(out Content content) => base.CanPop(out content) && !Contents.All(c => c == _expectedContent);

        public override bool CanPush(Space from, Content content) => base.CanPush(from, content) && content == _expectedContent && Contents.All(c => c == _expectedContent);

        public override Space Pop(out Content content, out int distance)
        {
            content = Contents.Last();
            distance = Capacity - Contents.Count + 1;
            return new SideRoom(_expectedContent, Capacity, Contents.Take(Contents.Count - 1).ToArray());
        }

        public override Space Push(Content content, out int distance)
        {
            distance = Capacity - Contents.Count;
            return new SideRoom(_expectedContent, Capacity, Contents.Append(content).ToArray());
}

        public override bool Equals(object? obj) => base.Equals(obj) && obj is SideRoom sideRoom && _expectedContent == sideRoom._expectedContent;

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), _expectedContent);
    }

    public class Hallway : Space
    {
        public Hallway(params Content[] contents) : base(1, contents)
        {
        }

        public override bool CanPush(Space from, Content content) => base.CanPush(from, content) && !from.IsHallway();

        public override Space Pop(out Content content, out int distance)
        {
            content = Contents.Last();
            distance = 0;
            return new Hallway(Contents.Take(Contents.Count - 1).ToArray());
        }

        public override Space Push(Content content, out int distance)
        {
            distance = 0;
            return new Hallway(Contents.Append(content).ToArray());
        }
    }

    public enum Content
    {
        None,
        A,
        B,
        C,
        D
    }
}
