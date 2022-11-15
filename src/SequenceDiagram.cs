namespace SequenceDiagram {
    interface IElement {
	public string render();
    }
    
    /* ==================== Base Elements ==================== */

    abstract class Base : IElement {
	abstract public string render();
    }

    // ============ Arrow ============

    abstract class Arrow : Base {
	override abstract public string render();
    }

    class SolidArrow : Arrow {
	override public string render() {return "TODO";}
    }

    /* ==================== Inductive Elements ==================== */

    abstract class Inductive : IElement {
	protected List<IElement> subcomponents;
	abstract public string render();
    }
    
    abstract class BlockSimple : Inductive {
	public BlockSimple add(IElement element) {
	    this.subcomponents.Add(element);
	    return this;
	}
	override abstract public string render();
    }
	
    class Optional : BlockSimple {
	override public string render() {return "TODO";}
    }

    class Loop : BlockSimple {
	public Loop() {
	    this.subcomponents = new List<IElement> {};
	}

	override public string render() {return "TODO";}
    }

    class Highlight : BlockSimple {
	public Highlight() {
	    this.subcomponents = new List<IElement> {};
	}

	override public string render() {return "TODO";}
    }


    abstract class BlockConditional: Inductive {
	public BlockConditional cond(IElement element) {
	    this.subcomponents.Add(element);
	    return this;
	}

	override abstract public string render();
    }

    class Alternative : BlockConditional {
	private string condition;
	public Alternative(string condition, params IElement[] subcomponents) {
	    this.condition = condition;
	    this.subcomponents = new List<IElement>();
	    foreach (IElement subcomponent in subcomponents) {
		this.subcomponents.Add(subcomponent);
	    }
	}

	override public string render() {return "TODO";}
    }

    class Parallel : BlockConditional {
	private string condition;

	public Parallel(string condition, params IElement[] subcomponents) {
	    this.condition = condition;
	    this.subcomponents = new List<IElement>();
	    foreach (IElement subcomponent in subcomponents) {
		this.subcomponents.Add(subcomponent);
	    }
	}
	
	override public string render() {return "TODO";}
    }

    class SequenceDiagram : IElement {
	private SequenceDiagram() {}
	public SequenceDiagram add(IElement element) { return new SequenceDiagram(); }

	public static SequenceDiagram sequenceDiagram() {
	    return new SequenceDiagram();
	}

	public string render() { return "TODO"; }
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

