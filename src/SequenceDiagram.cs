using System.Text;

namespace SequenceDiagram {
    interface IElement {
	public string render();
    }
    
    /* ==================== Base Elements ==================== */

    abstract class Base : IElement {
	abstract public string render();
    }

    // ============ Label ============
    
    abstract class Label : Base {
	override abstract public string render();
    }

    class Autonumber : Label {
	override public string render() {
	    return "autonumber";
	}
    }

    class Participants : Label {
	private List<string> participants;
	private Participants(List<string> participants) {
	    this.participants = participants;
	}

	override public string render() {
	    StringBuilder acc = new StringBuilder();
	    acc.Append("participants ");
	    foreach (string participant in this.participants) {
		acc.Append(participant);
		acc.Append(", ");
	    }
	    return acc.ToString().Trim(',');
	}
    }

    class Activate : Label {
	private string actor;
	private Activate(string actor) {
	    this.actor = actor;
	}
	override public string render() {
	    return "activate " + this.actor;
	}
    }
	
    class Deactivate : Label {
	private string actor;
	private Deactivate(string actor) {
	    this.actor = actor;
	}
	override public string render() {
	    return "deactivate " + this.actor;
	}
    }

    // ============ Arrow ============

    abstract class Arrow : Base {
	protected string from, to, message;
	protected bool activate, deactivate;
	protected Arrow(string from, string to, string message,
			bool activate = false, bool deactivate = false) {
	    (this.from, this.to, this.message, this.activate, this.deactivate) =
		(from, to, message, activate, deactivate);
	}
	override abstract public string render();
    }

    abstract class Solid : Arrow {
	protected Solid(string from, string to, string message,
			bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}
    }

    class SolidLine : Solid {
	private SolidLine(string from, string to, string message,
			  bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string render() {return "TODO";}
    }

    class SolidArrow : Solid {
	private SolidArrow(string from, string to, string message,
			   bool activate = false, bool deactivate = false) 
	    : base(from, to, message, activate, deactivate) {}

	override public string render() {return "TODO";}
    }

    class SolidCross : Solid {
	private SolidCross(string from, string to, string message,
			   bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string render() {return "TODO";}
    }

    class SolidOpen : Solid {
	private SolidOpen(string from, string to, string message,
			  bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string render() {return "TODO";}
    }

    abstract class Dotted : Arrow {
	protected Dotted(string from, string to, string message,
			 bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}
    }

    class DottedLine : Dotted {
	private DottedLine(string from, string to, string message,
			   bool activate = false, bool deactivate = false)

	    : base(from, to, message, activate, deactivate) {}

	override public string render() {return "TODO";}
    }

    class DottedArrow : Dotted {
	private DottedArrow(string from, string to, string message,
			    bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string render() {return "TODO";}
    }

    class DottedCross : Dotted {
	private DottedCross(string from, string to, string message,
			    bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string render() {return "TODO";}
    }

    class DottedOpen : Dotted {
	private DottedOpen(string from, string to, string message,
			   bool activate = false, bool deactivate = false)
	    : base(from, to, message, activate, deactivate) {}

	override public string render() {return "TODO";}
    }

    // ============ Note ============

    abstract class Note : Base {
	override abstract public string render();
    }

    class NoteLeft : Note {
	private string actor, note;
	private NoteLeft(string actor, string note) {
	    this.actor = actor;
	    this.note = note;
	}
	
	override public string render() {
	    return $"Note left of {this.actor}: {this.note}";
	}
    }

    class NoteRight : Note {
	private string actor, note;
	private NoteRight(string actor, string note) {
	    this.actor = actor;
	    this.note = note;
	}

	override public string render() {
	    return $"Note right of {this.actor}: {this.note}";
	}
    }
	
    class NoteOver : Note {
	private string actor1, note;
	private string? actor2;

	private NoteOver(string actor1, string note) {
	    this.actor1 = actor1;
	    this.note = note;
	}
	
	private NoteOver(string actor1, string actor2, string note) {
	    this.actor1 = actor1;
	    this.actor2 = actor2;
	    this.note = note;
	}

	override public string render(){
	    return this.actor2 == null ? $"Note over {this.actor1}: {this.note}" : $"Note over {this.actor1},{this.actor2}: {this.note}"; 
	}
    }
	
    /* ==================== Inductive Elements ==================== */

    abstract class Inductive : IElement {
	protected string detail;
	// protected List<object> subcomponents;
	protected Inductive(string detail) {
	    this.detail = detail;
	}
	abstract public string render();
    }
    
    abstract class BlockSimple : Inductive {
	protected List<IElement> subcomponents;
	protected BlockSimple(string detail) : base(detail) {
	    this.subcomponents = new List<IElement>();
	}
	public Inductive add(IElement element) {
	    this.subcomponents.Add(element);
	    return this;
	}
	override abstract public string render();
    }
	
    class Optional : BlockSimple {
	private Optional(string detail) : base(detail) {}
	override public string render() {return "TODO";}
    }

    class Loop : BlockSimple {
	private Loop(string detail) : base(detail) {}
	override public string render() {return "TODO";}
    }

    class Highlight : BlockSimple {
	private Highlight(string detail) : base(detail) {}
	override public string render() {return "TODO";}
    }


    abstract class BlockConditional: Inductive {
	protected List<(string, IElement)> subcomponents;
	protected BlockConditional(string condition, params IElement[] subcomponents) : base(condition) {
	    this.subcomponents = new List<(string, IElement)>();
	    foreach (IElement subcomponent in subcomponents) {
		this.subcomponents.Add((condition, subcomponent));
	    }
	}
	public BlockConditional cond(string condition, IElement element) {
	    this.subcomponents.Add((condition, element));
	    return this;
	}

	override abstract public string render();
    }


    class Alternative : BlockConditional {
	private Alternative(string condition, params IElement[] subcomponents) : base(condition, subcomponents) {}

	override public string render() {return "TODO";}
    }

    class Parallel : BlockConditional {
	private Parallel(string condition, params IElement[] subcomponents) : base(condition, subcomponents) {}
	
	override public string render() {return "TODO";}
    }

    class SequenceDiagram : BlockSimple {
	private SequenceDiagram() : base("") {}

	public static SequenceDiagram sequenceDiagram() {
	    return new SequenceDiagram();
	}

	override public string render() { return "TODO"; }
    }

}

// for increased challenge (and ergonomics), let's not use any sort of StringBuilder in .render(),
// but define it recursively where a render is a combination of
// the renders of its lower-elements.

// short sketch:
// SequenceDiagram sd = sequenceDiagram()
// 	        	.add(loop("until dead")
// 			     .add(solidArrow("alice", "bob", "TODO"))  // solidArrow should enable config by taking named & optional args
// 			     .add(solidArrow("bob", "alice", "hoho"))
// 			     .add(optional("hoho")
// 				     .add(solidArrow("alice", "bob", "TODO"))
// 				     .add(alternative("x = 1", solidArrow("alice", "bob", "TODO"))  // alternative should take variadic args
// 				                .cond("x = 2", solidArrow("bob", "join", "TODO"))
// 			   		        .cond("x = 3", solidArrow("john", "alice", "TODO"))))
// 	                     .add(parallel("alice to bob", solidArrow("alice", "bob", "TODO"))
// 		                     .cond("bob to alice", solidArrow("bob", "alice", "TODO"))))

