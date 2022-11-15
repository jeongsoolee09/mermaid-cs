namespace SequenceDiagram {
    interface IElement {
	public void add(IElement element)
    }
    
    abstract class Arrow : IElement {
	
    }

    class SolidArrow : Arrow, IElement {
	
    }

    class Loop {
	
    }
    
    class SequenceDiagram : IElement {
	public string render() {
	    return "Hihi";
	}
    }

}

// SequenceDiagram sd = sequenceDiagram()
// 	        	.add(loop("until dead")
// 			     .add(solidArrow("alice", "bob", "hihi"))
// 			     .add(solidArrow("bob", "alice", "hoho"))
// 			     .add(optional("hoho")
// 				     .add(solidArrow("alice", "bob", "hihi"))
// 				     .add(alternative("x = 1", solidArrow("alice", "bob", "hihi"))  // alternative takes variadic arguments
// 				                .cond("x = 2", solidArrow("bob", "join", "hihi"))
// 			   		        .cond("x = 3", solidArrow("john", "alice", "hihi"))))
// 	                     .add(parallel("alice to bob", solidArrow("alice", "bob", "hihi"))
// 		                     .cond("bob to alice", solidArrow("bob", "alice", "hihi"))))

