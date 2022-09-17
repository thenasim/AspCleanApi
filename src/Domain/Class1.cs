namespace Domain;

public class Class1
{
	private readonly Class2 _class2 = new();

	public string Hello(bool show)
	{
		if (show)
		{
			return "Yes";
		}

		return "No";
	}
}

public class Class2 { }
