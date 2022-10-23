![example workflow](https://github.com/github/docs/actions/workflows/main.yml/badge.svg)

# MED-EL Application Assignment 

The task is to create an assembly to encapsulate different vehicles from a client application. For that
purpose, you negotiated with the customer that there needs to be a method void Move()which
outputs on the debug line which vehicle you currently move, for example:

toyota.Move();
“You are driving a car from Toyota.”
honda.Move();
“You are driving a motorcycle from Honda.”

The customer further requested that the framework supports the following vehicles:
- Motorcycles fabricated by Honda
- Motorcycles fabricated by KTM.
- Cars fabricated by Toyota
- Cars fabricated by Honda

Finally, the customer requests to be able to switch the tires of cars, and also show which tires (including
its properties) a car uses by calling the Move()method. Summer tires are characterized by Pressure
and MaximumTemperature, whereas winter tires are characterized by Pressure,
MinimumTemperature and Thickness (assume all values are floats). Cars should come per default with
summer tires, where pressure is set to 2.5bar, and the maximum temperature to 50°C.

Implement the functionality by applying object-oriented principles in C#.
