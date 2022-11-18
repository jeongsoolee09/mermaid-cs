using Sequence;
using static Sequence.Lang;
using System;

string alice = "alice";
string bob = "bob";
string john = "john";

string sayhi = "hihi";
string sayho = "hoho";

SequenceDiagram sd = (SequenceDiagram) sequenceDiagram()
	        	.add(loop("until dead")
			     .add(solidArrow(alice, bob, sayhi))
			     .add(solidArrow(bob, alice, sayho))
			     .add(optional("hoho")
				     .add(solidArrow(alice, bob, sayhi))
				     .add(alternative("x = 1", solidArrow(alice, bob, sayhi))
				                .cond("x = 2", solidArrow(bob, john, sayhi))
			   		        .cond("x = 3", solidArrow(john, alice, sayhi))))
	                     .add(parallel("alice to bob", solidArrow(alice, bob, sayhi))
				  .cond("bob to alice", solidArrow(bob, alice, sayho))));


Console.WriteLine(sd);
