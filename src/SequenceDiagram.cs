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
	override public string render() {return "hihi";}
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
	override public string render() {return "hihi";}
    }

    class Loop : BlockSimple {
	public Loop() {
	    this.subcomponents = new List<IElement> {};
	}

	override public string render() {return "hihi";}
    }

    class Highlight : BlockSimple {
	public Highlight() {
	    this.subcomponents = new List<IElement> {};
	}

	override public string render() {return "hihi";}
    }


    abstract class BlockConditional: Inductive {
	public BlockConditional cond(IElement element) {
	    this.subcomponents.Add(element);
	    return this;
	}

	override abstract public string render();
    }

    class Alternative : BlockConditional {
	override public string render() {return "hihi";}
    }

    class Parallel : BlockConditional {
	override public string render() {return "hihi";}
    }

    class SequenceDiagram : IElement {
	public SequenceDiagram add(IElement element) { return new SequenceDiagram(); }

	public string render() { return "Hihi"; }
    }
}

// for increased challenge, let's not use any sort of StringBuilder in .render(),
// but define it recursively where a render is a combination of
// the renders of its lower-elements.

// short sketch:
// SequenceDiagram sd = sequenceDiagram()
// 	        	.add(loop("until dead")
// 			     .add(solidArrow("alice", "bob", "hihi"))  // solidArrow should enable config by taking named & optional args
// 			     .add(solidArrow("bob", "alice", "hoho"))
// 			     .add(optional("hoho")
// 				     .add(solidArrow("alice", "bob", "hihi"))
// 				     .add(alternative("x = 1", solidArrow("alice", "bob", "hihi"))  // alternative should take variadic args
// 				                .cond("x = 2", solidArrow("bob", "join", "hihi"))
// 			   		        .cond("x = 3", solidArrow("john", "alice", "hihi"))))
// 	                     .add(parallel("alice to bob", solidArrow("alice", "bob", "hihi"))
// 		                     .cond("bob to alice", solidArrow("bob", "alice", "hihi"))))

