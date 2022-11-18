using System.Text;

namespace Sequence {
    public interface IElement {}
    
    /* ==================== Base Elements ==================== */

    public abstract class Base : IElement {}

    // ============ Label ============
    
    public abstract class Label : Base {}

    public class Autonumber : Label {
	override public string ToString() {
	    return "autonumber";
	}
    }

    public class Participants : Label {
	private List<string> participants;
	public Participants(params string[] participants) {
	    this.participants = participants.ToList();
	}

	override public string ToString() {
	    StringBuilder acc = new StringBuilder();
	    acc.Append("participants ");
	    foreach (string participant in this.participants)
		acc.Append($"{participant}, ");
	    return acc.ToString().Trim(',');
	}
    }

    public class Activate : Label {
	private string actor;
	public Activate(string actor) {
	    this.actor = actor;
	}
	override public string ToString() {
	    return $"activate {this.actor}";
	}
    }
	
    public class Deactivate : Label {
	private string actor;
	public Deactivate(string actor) {
	    this.actor = actor;
	}
	override public string ToString() {
	    return $"deactivate {this.actor}";
	}
    }

    // ============ Arrow ============

    public abstract class Arrow : Base {
	protected string from, to, message;
	protected bool activate, deactivate;
	public Arrow(string from, string to, string message,
			bool activate = false, bool deactivate = false) {
	    (this.from, this.to, this.message, this.activate, this.deactivate) =
		(from, to, message, activate, deactivate);
	}
	override abstract public string ToString();
    }

    public abstract class Solid : Arrow {
	public Solid(string from, string to, string message,
			bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}
    }

    public class SolidLine : Solid {
	public SolidLine(string from, string to, string message,
			  bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string ToString() =>
	    (this.activate, this.deactivate) switch {
	    (false, false) => $"{this.from}->{this.to}: {this.message}",
	    (true, false)  => $"{this.from}->+{this.to}: {this.message}",
	    (false, true)  => $"{this.from}->-{this.to}: {this.message}",
	    (true, true)   => throw new ArgumentException("Cannot be both activate and deactivate receiver at the same time")
	};

    }

    public class SolidArrow : Solid {
	public SolidArrow(string from, string to, string message,
			   bool activate = false, bool deactivate = false) 
	    : base(from, to, message, activate, deactivate) {}

	override public string ToString() =>
	    (this.activate, this.deactivate) switch {
	    (false, false) => $"{this.from}->>{this.to}: {this.message}",
	    (true, false)  => $"{this.from}->>+{this.to}: {this.message}",
	    (false, true)  => $"{this.from}->>-{this.to}: {this.message}",
	    (true, true)   => throw new ArgumentException("Cannot be both activate and deactivate receiver at the same time")
	};

    }

    public class SolidCross : Solid {
	public SolidCross(string from, string to, string message,
			   bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string ToString() =>
	    (this.activate, this.deactivate) switch {
	    (false, false) => $"{this.from}-x{this.to}: {this.message}",
	    (true, false)  => $"{this.from}-x+{this.to}: {this.message}",
	    (false, true)  => $"{this.from}-x-{this.to}: {this.message}",
	    (true, true)   => throw new ArgumentException("Cannot be both activate and deactivate receiver at the same time")
	};
    }

    public class SolidOpen : Solid {
	public SolidOpen(string from, string to, string message,
			  bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string ToString() =>
	    (this.activate, this.deactivate) switch {
	    (false, false) => $"{this.from}-){this.to}: {this.message}",
	    (true, false)  => $"{this.from}-)+{this.to}: {this.message}",
	    (false, true)  => $"{this.from}-)-{this.to}: {this.message}",
	    (true, true)   => throw new ArgumentException("Cannot be both activate and deactivate receiver at the same time")
	};
    }

    public abstract class Dotted : Arrow {
	public Dotted(string from, string to, string message,
			 bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}
    }

    public class DottedLine : Dotted {
	public DottedLine(string from, string to, string message,
			   bool activate = false, bool deactivate = false)

	    : base(from, to, message, activate, deactivate) {}

	override public string ToString() =>
	    (this.activate, this.deactivate) switch {
	    (false, false) => $"{this.from}-->{this.to}: {this.message}",
	    (true, false)  => $"{this.from}-->+{this.to}: {this.message}",
	    (false, true)  => $"{this.from}-->-{this.to}: {this.message}",
	    (true, true)   => throw new ArgumentException("Cannot be both activate and deactivate receiver at the same time")
	};
    }

    public class DottedArrow : Dotted {
	public DottedArrow(string from, string to, string message,
			    bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string ToString() =>
	    (this.activate, this.deactivate) switch {
	    (false, false) => $"{this.from}-->>{this.to}: {this.message}",
	    (true, false)  => $"{this.from}-->>+{this.to}: {this.message}",
	    (false, true)  => $"{this.from}-->>-{this.to}: {this.message}",
	    (true, true)   => throw new ArgumentException("Cannot be both activate and deactivate receiver at the same time")
	};
    }

    public class DottedCross : Dotted {
	public DottedCross(string from, string to, string message,
			    bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string ToString() =>
	    (this.activate, this.deactivate) switch {
	    (false, false) => $"{this.from}--x{this.to}: {this.message}",
	    (true, false)  => $"{this.from}--x+{this.to}: {this.message}",
	    (false, true)  => $"{this.from}--x-{this.to}: {this.message}",
	    (true, true)   => throw new ArgumentException("Cannot be both activate and deactivate receiver at the same time")
	};
    }

    public class DottedOpen : Dotted {
	public DottedOpen(string from, string to, string message,
			   bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string ToString() =>
	    (this.activate, this.deactivate) switch {
	    (false, false) => $"{this.from}--){this.to}: {this.message}",
	    (true, false)  => $"{this.from}--)+{this.to}: {this.message}",
	    (false, true)  => $"{this.from}--)-{this.to}: {this.message}",
	    (true, true)   => throw new ArgumentException("Cannot be both activate and deactivate receiver at the same time")
	};
    }

    // ============ Note ============

    public abstract class Note : Base {
	override abstract public string ToString();
    }

    public class NoteLeft : Note {
	private string actor, note;
	public NoteLeft(string actor, string note) {
	    this.actor = actor;
	    this.note = note;
	}
	
	override public string ToString() {
	    return $"Note left of {this.actor}: {this.note}";
	}
    }

    public class NoteRight : Note {
	private string actor, note;
	public NoteRight(string actor, string note) {
	    this.actor = actor;
	    this.note = note;
	}

	override public string ToString() {
	    return $"Note right of {this.actor}: {this.note}";
	}
    }
	
    public class NoteOver : Note {
	private string actor1, note;
	private string? actor2;

	public NoteOver(string actor1, string note) {
	    this.actor1 = actor1;
	    this.note = note;
	}
	
	public NoteOver(string actor1, string actor2, string note) {
	    this.actor1 = actor1;
	    this.actor2 = actor2;
	    this.note = note;
	}

	override public string ToString(){
	    return this.actor2 == null ? $"Note over {this.actor1}: {this.note}" : $"Note over {this.actor1},{this.actor2}: {this.note}"; 
	}
    }
	
    /* ==================== Inductive Elements ==================== */

    public abstract class Inductive : IElement {
	protected string detail;
	public Inductive(string detail) {
	    this.detail = detail;
	}
	public abstract Inductive add(IElement element);
	public abstract Inductive cond(string condition, IElement element);
    }
    
    public abstract class BlockSimple : Inductive {
	protected List<IElement> subcomponents;
	protected string blockPrefix;

	public BlockSimple(string detail) : base(detail) {
	    this.blockPrefix = "";
	    this.subcomponents = new List<IElement>();
	}

	override public Inductive add(IElement element) {
	    this.subcomponents.Add(element);
	    return this;
	}

	override public Inductive cond(string condition, IElement element) => throw new ArgumentException($"Cannot call cond on {this.blockPrefix}");

	override public string ToString() {
	    StringBuilder acc = new StringBuilder();
	    acc.Append($"{this.blockPrefix} {this.detail}\n");
	    foreach (IElement subcomponent in this.subcomponents)
		acc.Append($"    {subcomponent.ToString()}\n");
	    acc.Append("end");
	    return acc.ToString();
	}
    }
	
    public class Optional : BlockSimple {
	public Optional(string detail) : base(detail) {
	    this.blockPrefix = "opt"; 
	}
    }

    public class Loop : BlockSimple {
	public Loop(string detail) : base(detail) {
	    this.blockPrefix = "loop";
	}
    }

    public class Highlight : BlockSimple {
	public Highlight(string color) : base(color) {
	    this.blockPrefix = "rect";
	}
    }


    public abstract class BlockConditional: Inductive {
	protected List<(string, IElement)> subcomponents;
	protected string blockPrefix;
	protected string condKeyword;

	public BlockConditional(string condition, params IElement[] subcomponents) : base(condition) {
	    this.blockPrefix = "";
	    this.condKeyword = "";
	    this.subcomponents = new List<(string, IElement)>();
	    foreach (IElement subcomponent in subcomponents)
		this.subcomponents.Add((condition, subcomponent));
	}

	override public Inductive add(IElement element) => throw new ArgumentException($"Cannot call add on {this.blockPrefix}");

	override public Inductive cond(string condition, IElement element) {
	    this.subcomponents.Add((condition, element));
	    return this;
	}

	override public string ToString() {
	    StringBuilder acc = new StringBuilder();
	    acc.Append($"{this.blockPrefix} {this.detail}\n");
	    foreach ((string condition, IElement subcomponent) in this.subcomponents)
		acc.Append($"    {this.condKeyword} {condition}\n    {subcomponent.ToString()}\n");
	    acc.Append("end");
	    return acc.ToString();
	}
    }


    public class Alternative : BlockConditional {
	public Alternative(string condition, params IElement[] subcomponents) : base(condition, subcomponents) {
	    this.blockPrefix = "alt";
	    this.condKeyword = "else";
	}
    }

    public class Parallel : BlockConditional {
	public Parallel(string condition, params IElement[] subcomponents) : base(condition, subcomponents) {
	    this.blockPrefix = "parallel";
	    this.condKeyword = "and";
	}
    }

    public class SequenceDiagram : BlockSimple {
	public SequenceDiagram() : base("") {
	    this.blockPrefix = "sequenceDiagram";
	}
    }
    
    public static class Lang {


	public static Autonumber autonumber() => new Autonumber();
	public static Participants participants(params string[] participants) => new Participants(participants);
	public static Activate activate(string actor) => new Activate(actor);
	public static Deactivate deactivate(string actor) => new Deactivate(actor);
	public static SolidLine solidLine(string from, string to, string message,
					    bool activate = false, bool deactivate = false) =>
	    new SolidLine(from, to, message, activate, deactivate);
	public static SolidArrow solidArrow(string from, string to, string message,
					    bool activate = false, bool deactivate = false) =>
	    new SolidArrow(from, to, message, activate, deactivate);
	public static SolidCross solidCross(string from, string to, string message,
					    bool activate = false, bool deactivate = false) =>
	    new SolidCross(from, to, message, activate, deactivate);
	public static SolidOpen solidOpen(string from, string to, string message,
					    bool activate = false, bool deactivate = false) =>
	    new SolidOpen(from, to, message, activate, deactivate);
	public static DottedLine dottedLine(string from, string to, string message,
					    bool activate = false, bool deactivate = false) =>
	    new DottedLine(from, to, message, activate, deactivate);
	public static DottedArrow dottedArrow(string from, string to, string message,
					    bool activate = false, bool deactivate = false) =>
	    new DottedArrow(from, to, message, activate, deactivate);
	public static DottedCross dottedCross(string from, string to, string message,
					    bool activate = false, bool deactivate = false) =>
	    new DottedCross(from, to, message, activate, deactivate);
	public static DottedOpen dottedOpen(string from, string to, string message,
					    bool activate = false, bool deactivate = false) =>
	    new DottedOpen(from, to, message, activate, deactivate);
	public static NoteLeft noteLeft(string actor, string note) => new NoteLeft(actor, note);
	public static NoteRight noteRight(string actor, string note) => new NoteRight(actor, note);
	public static NoteOver noteOver(string actor1, string note) => new NoteOver(actor1, note);
	public static NoteOver noteOver(string actor1, string actor2, string note) => new NoteOver(actor1, actor2, note);
	public static Optional optional(string detail) => new Optional(detail);
	public static Loop loop(string detail) => new Loop(detail);
	public static Highlight highlight(string detail) => new Highlight(detail);
	public static Alternative alternative(string condition, params IElement[] subcomponents) => new Alternative(condition, subcomponents);
	public static Parallel parallel(string condition, params IElement[] subcomponents) => new Parallel(condition, subcomponents);
	public static SequenceDiagram sequenceDiagram() => new SequenceDiagram();
    }
}
