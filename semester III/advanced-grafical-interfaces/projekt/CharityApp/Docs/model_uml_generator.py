from graphviz import Digraph

dot = Digraph('UML_CharityApp', format='png')

dot.node('SuperAdmin', '''SuperAdmin
------------------------
+ Credentials: UserCredentials
+ Admins: List<Admin>
+ Goals: List<Goal>
+ TotalAmount: double
''')

dot.node('Admin', '''Admin
------------------------
+ Credentials: UserCredentials
+ Volunteers: List<Volunteer>
+ SuperAdmin: SuperAdmin
+ FirstName: string
+ LastName: string
+ Pesel: string
+ Region: string
''')

dot.node('Volunteer', '''Volunteer
------------------------
+ Name: string
+ Location: string
+ DateAndTime: DateTime
+ Credentials: UserCredentials
+ CollectedAmount: double
+ IsSettled: bool
''')

dot.node('Goal', '''Goal
------------------------
+ Id: Guid
+ Name: string
+ Description: string
+ Amount: double
+ AccountNumber: string
+ IsConfirmed: bool
''')

dot.node('UserCredentials', '''UserCredentials
------------------------
+ Username: string
+ Password: string
''')

dot.node('Currency', '''Currency
------------------------
+ Code: CurrencyCode
+ ConversionFactor: double
+ ConvertToPLN(CurrencyCode, double): double
''')


dot.edge('SuperAdmin', 'Goal', label='uses')
dot.edge('SuperAdmin', 'Admin', label='uses')
dot.edge('Admin', 'Volunteer', label='uses')
dot.edge('Admin', 'UserCredentials', label='uses')
dot.edge('SuperAdmin', 'UserCredentials', label='uses')
dot.edge('Volunteer', 'UserCredentials', label='uses')
dot.edge('Admin', 'SuperAdmin', label='relates to', dir='both')

output_path = 'UML_CharityApp.png'
dot.render(output_path[:-4])

output_path

