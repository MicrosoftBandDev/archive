using Microsoft.Band;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace BandCalc.WindowsApp
{
	internal class CalcMain
	{
		private readonly PageLayout pageLayout;
		private readonly PageLayoutData pageLayoutData;
		
		private readonly FlowPanel panel = new FlowPanel();
		private readonly FlowPanel panel2 = new FlowPanel();
		private readonly TextBlock textBlock = new TextBlock();
		private readonly Icon icon = new Icon();
		private readonly FlowPanel panel3 = new FlowPanel();
		internal TextBlock CalcResult = new TextBlock();
		private readonly FlowPanel panel4 = new FlowPanel();
		private readonly TextButton button = new TextButton();
		private readonly TextButton button2 = new TextButton();
		private readonly TextButton button3 = new TextButton();
		
		private readonly TextBlockData textBlockData = new TextBlockData(70, "Band Calculator");
		private readonly IconData iconData = new IconData(66, 1);
		internal TextBlockData CalcResultData = new TextBlockData(78, "0");
		private readonly TextButtonData buttonData = new TextButtonData(20, "=");
		private readonly TextButtonData button2Data = new TextButtonData(19, "c");
		private readonly TextButtonData button3Data = new TextButtonData(27, "del");
		
		public CalcMain()
		{
			LoadIconMethod = LoadIcon;
			AdjustUriMethod = (uri) => uri;
			
			panel = new FlowPanel();
			panel.Orientation = FlowPanelOrientation.Vertical;
			panel.Rect = new PageRect(0, 0, 262, 128);
			panel.ElementId = 79;
			panel.Margins = new Margins(0, 0, 0, 0);
			panel.HorizontalAlignment = HorizontalAlignment.Left;
			panel.VerticalAlignment = VerticalAlignment.Top;
			
			panel2 = new FlowPanel();
			panel2.Orientation = FlowPanelOrientation.Horizontal;
			panel2.Rect = new PageRect(0, 0, 240, 45);
			panel2.ElementId = 75;
			panel2.Margins = new Margins(0, 0, 0, 0);
			panel2.HorizontalAlignment = HorizontalAlignment.Left;
			panel2.VerticalAlignment = VerticalAlignment.Top;
			
			textBlock = new TextBlock();
			textBlock.Font = TextBlockFont.Small;
			textBlock.Baseline = 0;
			textBlock.BaselineAlignment = TextBlockBaselineAlignment.Automatic;
			textBlock.AutoWidth = true;
			textBlock.ColorSource = ElementColorSource.BandBase;
			textBlock.Rect = new PageRect(0, 0, 188, 30);
			textBlock.ElementId = 70;
			textBlock.Margins = new Margins(10, 1, 0, 0);
			textBlock.HorizontalAlignment = HorizontalAlignment.Left;
			textBlock.VerticalAlignment = VerticalAlignment.Top;
			
			panel2.Elements.Add(textBlock);
			
			icon = new Icon();
			icon.ColorSource = ElementColorSource.BandBase;
			icon.Rect = new PageRect(0, 0, 32, 32);
			icon.ElementId = 66;
			icon.Margins = new Margins(10, 8, 0, 0);
			icon.HorizontalAlignment = HorizontalAlignment.Left;
			icon.VerticalAlignment = VerticalAlignment.Top;
			
			panel2.Elements.Add(icon);
			
			panel.Elements.Add(panel2);
			
			panel3 = new FlowPanel();
			panel3.Orientation = FlowPanelOrientation.Horizontal;
			panel3.Rect = new PageRect(0, 0, 240, 35);
			panel3.ElementId = 74;
			panel3.Margins = new Margins(0, 0, 0, 0);
			panel3.HorizontalAlignment = HorizontalAlignment.Left;
			panel3.VerticalAlignment = VerticalAlignment.Center;
			
			CalcResult = new TextBlock();
			CalcResult.Font = TextBlockFont.Small;
			CalcResult.Baseline = 0;
			CalcResult.BaselineAlignment = TextBlockBaselineAlignment.Automatic;
			CalcResult.AutoWidth = true;
			CalcResult.ColorSource = ElementColorSource.Custom;
			CalcResult.Color = new BandColor(255, 255, 255);
			CalcResult.Rect = new PageRect(0, 0, 32, 32);
			CalcResult.ElementId = 78;
			CalcResult.Margins = new Margins(10, 0, 0, 0);
			CalcResult.HorizontalAlignment = HorizontalAlignment.Left;
			CalcResult.VerticalAlignment = VerticalAlignment.Center;
			
			panel3.Elements.Add(CalcResult);
			
			panel.Elements.Add(panel3);
			
			panel4 = new FlowPanel();
			panel4.Orientation = FlowPanelOrientation.Horizontal;
			panel4.Rect = new PageRect(0, 0, 250, 45);
			panel4.ElementId = 76;
			panel4.Margins = new Margins(0, 0, 0, 0);
			panel4.HorizontalAlignment = HorizontalAlignment.Left;
			panel4.VerticalAlignment = VerticalAlignment.Top;
			
			button = new TextButton();
			button.PressedColor = new BandColor(0, 121, 214);
			button.Rect = new PageRect(0, 0, 85, 35);
			button.ElementId = 20;
			button.Margins = new Margins(10, 10, 0, 0);
			button.HorizontalAlignment = HorizontalAlignment.Center;
			button.VerticalAlignment = VerticalAlignment.Top;
			
			panel4.Elements.Add(button);
			
			button2 = new TextButton();
			button2.PressedColor = new BandColor(0, 121, 214);
			button2.Rect = new PageRect(0, 0, 60, 35);
			button2.ElementId = 19;
			button2.Margins = new Margins(10, 10, 0, 0);
			button2.HorizontalAlignment = HorizontalAlignment.Center;
			button2.VerticalAlignment = VerticalAlignment.Top;
			
			panel4.Elements.Add(button2);
			
			button3 = new TextButton();
			button3.PressedColor = new BandColor(0, 121, 214);
			button3.Rect = new PageRect(0, 0, 60, 35);
			button3.ElementId = 27;
			button3.Margins = new Margins(10, 10, 0, 0);
			button3.HorizontalAlignment = HorizontalAlignment.Center;
			button3.VerticalAlignment = VerticalAlignment.Top;
			
			panel4.Elements.Add(button3);
			
			panel.Elements.Add(panel4);
			pageLayout = new PageLayout(panel);
			
			PageElementData[] pageElementDataArray = new PageElementData[6];
			pageElementDataArray[0] = textBlockData;
			pageElementDataArray[1] = iconData;
			pageElementDataArray[2] = CalcResultData;
			pageElementDataArray[3] = buttonData;
			pageElementDataArray[4] = button2Data;
			pageElementDataArray[5] = button3Data;
			
			pageLayoutData = new PageLayoutData(pageElementDataArray);
		}
		
		public PageLayout Layout
		{
			get
			{
				return pageLayout;
			}
		}
		
		public PageLayoutData Data
		{
			get
			{
				return pageLayoutData;
			}
		}
		
		public Func<string, Task<BandIcon>> LoadIconMethod
		{
			get;
			set;
		}
		
		public Func<string, string> AdjustUriMethod
		{
			get;
			set;
		}
		
		private static async Task<BandIcon> LoadIcon(string uri)
		{
			StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));
			
			using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.Read))
			{
				WriteableBitmap bitmap = new WriteableBitmap(1, 1);
				await bitmap.SetSourceAsync(fileStream);
				return bitmap.ToBandIcon();
			}
		}
		
		public async Task LoadIconsAsync(BandTile tile)
		{
			int firstIconIndex = tile.AdditionalIcons.Count + 2; // First 2 are used by the Tile itself
			tile.AdditionalIcons.Add(await LoadIconMethod(AdjustUriMethod("ms-appx:///Assets/SampleTileIconSmall.png")));
			pageLayoutData.ById<IconData>(66).IconIndex = (ushort)(firstIconIndex + 0);
		}
		
		public static BandTheme GetBandTheme()
		{
			var theme = new BandTheme();
			theme.Base = new BandColor(51, 102, 204);
			theme.HighContrast = new BandColor(58, 120, 221);
			theme.Highlight = new BandColor(58, 120, 221);
			theme.Lowlight = new BandColor(49, 101, 186);
			theme.Muted = new BandColor(43, 90, 165);
			theme.SecondaryText = new BandColor(137, 151, 171);
			return theme;
		}
		
		public static BandTheme GetTileTheme()
		{
			var theme = new BandTheme();
			theme.Base = new BandColor(51, 102, 204);
			theme.HighContrast = new BandColor(58, 120, 221);
			theme.Highlight = new BandColor(58, 120, 221);
			theme.Lowlight = new BandColor(49, 101, 186);
			theme.Muted = new BandColor(43, 90, 165);
			theme.SecondaryText = new BandColor(137, 151, 171);
			return theme;
		}
		
		public class PageLayoutData
		{
			private readonly PageElementData[] array;
			
			public PageLayoutData(PageElementData[] pageElementDataArray)
			{
				array = pageElementDataArray;
			}
			
			public int Count
			{
				get
				{
					return array.Length;
				}
			}
			
			public T Get<T>(int i) where T : PageElementData
			{
				return (T)array[i];
			}
			
			public T ById<T>(short id) where T:PageElementData
			{
				return (T)array.FirstOrDefault(elm => elm.ElementId == id);
			}
			
			public PageElementData[] All
			{
				get
				{
					return array;
				}
			}
		}
		
	}
}
