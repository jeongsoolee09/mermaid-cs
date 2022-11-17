using Sequence;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


SequenceDiagram sd = sequenceDiagram()
	        	.add(loop("until dead")
			     .add(solidArrow("alice", "bob", "hihi"))
			     .add(solidArrow("bob", "alice", "hoho"))
			     .add(optional("hoho")
				     .add(solidArrow("alice", "bob", "hihi"))
				     .add(alternative("x = 1", solidArrow("alice", "bob", "hihi"))
				                .cond("x = 2", solidArrow("bob", "join", "hihi"))
			   		        .cond("x = 3", solidArrow("john", "alice", "hihi"))))
	                     .add(parallel("alice to bob", solidArrow("alice", "bob", "hihi"))
				  .cond("bob to alice", solidArrow("bob", "alice", "hihi"))));
