using System.Collections.Generic;

//Port from java version
//http://snowball.tartarus.org/otherlangs/
//Algorithm
//http://snowball.tartarus.org/algorithms/portuguese/stemmer.html

namespace ConsoleApplication
{
	public class PortugueseStemmer {

		public string WordStemming(string word)
		{
			return algorithm(word);
		}

		private string algorithm(string st)
		{
			st = processNasalidedVowels(st);
			string stem = st;
			string r1 = findR(stem);
			string r2 = findR(r1);
			string rv = findRV(stem);

			stem = step1(stem,r1,r2,rv);

			if(stem.CompareTo(st) == 0)
				stem = step2(stem,r1,r2,rv);
			else
			{
				r1 = findR(stem);
				r2 = findR(r1);
				rv = findRV(stem);
			}

			if(stem.CompareTo(st)!=0)
			{
				r1 = findR(stem);
				r2 = findR(r1);
				rv = findRV(stem);
				stem = step3(stem,r1,r2,rv);
			}
			else
				stem = step4(stem,r1,r2,rv);

			if(stem.CompareTo(st)!=0)
			{
				r1 = findR(stem);
				r2 = findR(r1);
				rv = findRV(stem);
			}

			stem = step5(stem,r1,r2,rv);

			stem = deprocessNasalidedVowels(stem);

			return stem;
		}

		private string step1(string st, string r1, string r2, string rv)
		{
			int i;

			for(i = 0; i<suffix1.Length; i++)	//Rule 1
				if(r2.EndsWith(suffix1[i]))
					return st.Substring(0, st.Length -suffix1[i].Length);

			for(i = 0; i<suffix2.Length; i++)	//Rule 2
				if(r2.EndsWith(suffix2[i]))
					return st.Substring(0, st.Length - suffix2[i].Length)+"log";

			for(i = 0; i<suffix3.Length; i++)	//Rule 3
				if(r2.EndsWith(suffix3[i]))
					return st.Substring(0, st.Length-suffix3[i].Length)+"u";

			for(i = 0; i<suffix4.Length; i++)	//Rule 4
				if(r2.EndsWith(suffix4[i]))
					return st.Substring(0, st.Length-suffix4[i].Length)+"ente";

			for(i = 0; i<suffix5.Length; i++)	//Rule 5
				if(r1.EndsWith(suffix5[i]))
				{
					st = st.Substring(0, st.Length-suffix5[i].Length);
					if(st.EndsWith("iv") && r2.EndsWith("iv"+suffix5[i]))
					{
						st = st.Substring(0,st.Length-2);
						if(st.EndsWith("at")&& r2.EndsWith("ativ"+suffix5[i]))
							st = st.Substring(0,st.Length-2);
					}
					else if(st.EndsWith("os") && r2.EndsWith("os"+suffix5[i]))
						st = st.Substring(0,st.Length-2);
					else if(st.EndsWith("ic") && r2.EndsWith("ic"+suffix5[i]))
						st = st.Substring(0,st.Length-2);
					else if(st.EndsWith("ad") && r2.EndsWith("ad"+suffix5[i]))
						st = st.Substring(0,st.Length-2);
					return st;
				}

			for(i = 0; i<suffix6.Length; i++)	//Rule 6
				if(r2.EndsWith(suffix6[i]))
				{
					st = st.Substring(0, st.Length-suffix6[i].Length);
					if(st.EndsWith("ante") && r2.EndsWith("ante"+suffix6[i]))
						st = st.Substring(0,st.Length-4);
					else if(st.EndsWith("avel") && r2.EndsWith("avel"+suffix6[i]))
						st = st.Substring(0,st.Length-4);
					else if(st.EndsWith("ível") && r2.EndsWith("ível"+suffix6[i]))
						st = st.Substring(0,st.Length-4);
					return st;
				}

			for(i = 0; i<suffix7.Length; i++)	//Rule 7
				if(r2.EndsWith(suffix7[i]))
				{
					st = st.Substring(0, st.Length-suffix7[i].Length);
					if(st.EndsWith("abil") && r2.EndsWith("abil"+suffix7[i]))
						st = st.Substring(0,st.Length-4);
					else if(st.EndsWith("ic") && r2.EndsWith("ic"+suffix7[i]))
						st = st.Substring(0,st.Length-2);
					else if(st.EndsWith("iv") && r2.EndsWith("iv"+suffix7[i]))
						st = st.Substring(0,st.Length-2);
					return st;
				}

			for(i = 0; i<suffix8.Length; i++)	//Rule 8
				if(r2.EndsWith(suffix8[i]))
				{
					st = st.Substring(0, st.Length-suffix8[i].Length);
					if(st.EndsWith("at") && r2.EndsWith("at"+suffix8[i]))
						st = st.Substring(0,st.Length-2);
					return st;
				}

			for(i = 0; i<suffix9.Length; i++)	//Rule 9
				if(rv.EndsWith(suffix9[i]))
				{
					if(st.EndsWith("e"+suffix9[i]))
						st = st.Substring(0,st.Length-suffix9[i].Length)+"ir";
					return st;
				}
			return st;
		}

		private string step2(string st, string r1, string r2, string rv)
		{
			for(int i = 0; i<suffixv.Length; i++)	//Rule 1
				if(rv.EndsWith(suffixv[i]))
					return st.Substring(0, st.Length-suffixv[i].Length);
			return st;
		}

		private string step3(string st, string r1, string r2, string rv)
		{
			if(rv.EndsWith("i")&&st.EndsWith("ci")) 	//Rule 1
				return st.Substring(0, st.Length-1);
			else
				return st;
		}

		private string step4(string st, string r1, string r2, string rv)
		{
			for(int i = 0; i<suffixr.Length; i++)	//Rule 1
				if(rv.EndsWith(suffixr[i]))
					return st.Substring(0,st.Length-suffixr[i].Length);
			return st;
		}

		private string step5(string st, string r1, string r2, string rv)
		{
			for(int i = 0; i<suffixf.Length; i++)	//Rule 1
				if(rv.EndsWith(suffixf[i]))
				{
					st = st.Substring(0,st.Length-suffixf[i].Length);
					if(st.EndsWith("gu") && rv.EndsWith("u"+suffixf[i]))
						st = st.Substring(0,st.Length-1);
					else if(st.EndsWith("ci") && rv.EndsWith("i"+suffixf[i]))
						st = st.Substring(0,st.Length-1);
					return st;
				}

			if(st.EndsWith("ç"))
				st = st.Substring(0,st.Length-1)+"c";
			return st;
		}


		private string findR(string st)
		{
			for(int i = 0; i< st.Length-1; i++)
				if(vowels.Contains(st[i]))
					if(!vowels.Contains(st[i+1]))
						return st.Substring(i+2);
			return "";
		}

		private string findRV(string st)
		{
			if(st.Length>2)
			{
				if(!vowels.Contains(st[1]))
				{
					for(int i=2; i<st.Length-1; i++)
						if(vowels.Contains(st[i]))
							return st.Substring(i+1);
				}
				else if(vowels.Contains(st[0]) && vowels.Contains(st[1]))
				{
					for(int i=2; i<st.Length-1; i++)
						if(!vowels.Contains(st[i]))
							return st.Substring(i+1);
				}
				else
					return st.Substring(3);
			}
			return "";
		}

		private string processNasalidedVowels(string st)
		{
			st = st.Replace("ã", "a~");
			st = st.Replace("õ", "o~");
			return st;
		}

		private string deprocessNasalidedVowels(string st)
		{
			st = st.Replace("a~", "ã");
			st = st.Replace("o~", "õ");
			return st;
		}

		private List<char> vowels = new List<char>(){'a', 'e', 'i', 'o', 'u', 'á', 'é', 'í', 'ó', 'ú', 'â', 'ê', 'ô'};

		private string[] suffix1 = {"amentos", "imentos", "amento", "imento", "adoras", "adores", "aço~es", "ismos", "istas", "adora", "aça~o", "antes", "ância", "ezas", "icos", "icas", "ismo", "ável", "ível", "ista", "osos", "osas", "ador", "ante", "eza", "ico", "ica", "oso", "osa"};
		private string[] suffix2 ={"logías", "logía"};
		private string[] suffix3 ={"uciones", "ución"};
		private string[] suffix4 ={"ências", "ência"};
		private string[] suffix5 ={"amente"};
		private string[] suffix6 ={"mente"};
		private string[] suffix7 ={"idades", "idade"};
		private string[] suffix8 ={"ivas", "ivos", "iva", "ivo"};
		private string[] suffix9 ={"iras", "ira"};
		private string[] suffixv = {"aríamos", "eríamos", "iríamos", "ássemos", "êssemos", "íssemos", "aríeis", "eríeis", "iríeis", "ásseis", "ésseis", "ísseis", "áramos", "éramos", "íramos", "ávamos", "aremos", "eremos", "iremos", "ariam", "eriam", "iriam", "assem", "essem", "issem", "ara~o", "era~o", "ira~o", "arias", "erias", "irias", "ardes", "erdes", "irdes", "asses", "esses", "isses", "astes", "estes", "istes", "áreis", "areis", "éreis", "ereis", "íreis", "ireis", "áveis", "íamos", "armos", "ermos", "irmos", "aria", "eria", "iria", "asse", "esse", "isse", "aste", "este", "iste", "arei", "erei", "irei", "aram", "eram", "iram", "avam", "arem", "erem", "irem", "ando", "endo", "indo", "adas", "idas", "arás", "aras", "erás", "eras", "irás", "avas", "ares", "eres", "ires", "íeis", "ados", "idos", "ámos", "amos", "emos", "imos", "iras", "ada", "ida", "ará", "ara", "erá", "era", "irá", "ava", "iam", "ado", "ido", "ias", "ais", "eis", "ira", "ia", "ei", "am", "em", "ar", "er", "ir", "as", "es", "is", "eu", "iu", "ou"};
		private string[] suffixr = {"os", "a", "i", "o", "á", "í", "ó"};
		private string[] suffixf = {"e", "é", "ê"};

	}
}