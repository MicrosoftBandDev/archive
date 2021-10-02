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
	internal class Options
	{
		private readonly PageLayout pageLayout;
		private readonly PageLayoutData pageLayoutData;
		
		private readonly FlowPanel panel = new FlowPanel();
		private readonly FlowPanel panel2 = new FlowPanel();
		private readonly TextBlock textBlock = new TextBlock();
		private readonly FlowPanel panel3 = new FlowPanel();
		private readonly TextBlock textBlock2 = new TextBlock();
		private readonly FlowPanel panel4 = new FlowPanel();
		private readonly TextButton button = new TextButton();
		
		private readonly TextBlockData textBlockData = new TextBlockData(53, "Settings");
		private readonly TextBlockData textBlock2Data = new TextBlockData(32, "Haptic feedback:");
		private readonly TextButtonData buttonData = new TextButtonData(33, "switch on/off");
		
		public Options()
		{
			LoadIconMethod = LoadIcon;
			AdjustUriMethod = (uri) => uri;
			
			panel = new FlowPanel();
			panel.Orientation = FlowPanelOrientation.Vertical;
			panel.Rect = new PageRect(0, 0, 262, 128);
			panel.ElementId = 96;
			panel.Margins = new Margins(0, 0, 0, 0);
			panel.HorizontalAlignment = HorizontalAlignment.Left;
			panel.VerticalAlignment = VerticalAlignment.Top;
			
			panel2 = new FlowPanel();
			panel2.Orientation = FlowPanelOrientation.Horizontal;
			panel2.Rect = new PageRect(0, 0, 240, 45);
			panel2.ElementId = 51;
			panel2.Margins = new Margins(0, 0, 0, 0);
			panel2.HorizontalAlignment = HorizontalAlignment.Left;
			panel2.VerticalAlignment = VerticalAlignment.Top;
			
			textBlock = new TextBlock();
			textBlock.Font = TextBlockFont.Small;
			textBlock.Baseline = 0;
			textBlock.BaselineAlignment = TextBlockBaselineAlignment.Automatic;
			textBlock.AutoWidth = true;
			textBlock.ColorSource = ElementColorSource.BandBase;
			textBlock.Rect = new PageRect(0, 0, 32, 40);
			textBlock.ElementId = 53;
			textBlock.Margins = new Margins(10, 1, 0, 0);
			textBlock.HorizontalAlignment = HorizontalAlignment.Left;
			textBlock.VerticalAlignment = VerticalAlignment.Top;
			
			panel2.Elements.Add(textBlock);
			
			panel.Elements.Add(panel2);
			
			panel3 = new FlowPanel();
			panel3.Orientation = FlowPanelOrientation.Horizontal;
			panel3.Rect = new PageRect(0, 0, 240, 45);
			panel3.ElementId = 97;
			panel3.Margins = new Margins(0, 0, 0, 0);
			panel3.HorizontalAlignment = HorizontalAlignment.Left;
			panel3.VerticalAlignment = VerticalAlignment.Top;
			
			textBlock2 = new TextBlock();
			textBlock2.Font = TextBlockFont.Small;
			textBlock2.Baseline = 0;
			textBlock2.BaselineAlignment = TextBlockBaselineAlignment.Automatic;
			textBlock2.AutoWidth = true;
			textBlock2.ColorSource = ElementColorSource.Custom;
			textBlock2.Color = new BandColor(255, 255, 255);
			textBlock2.Rect = new PageRect(0, 0, 32, 32);
			textBlock2.ElementId = 32;
			textBlock2.Margins = new Margins(10, 10, 0, 0);
			textBlock2.HorizontalAlignment = HorizontalAlignment.Left;
			textBlock2.VerticalAlignment = VerticalAlignment.Center;
			
			panel3.Elements.Add(textBlock2);
			
			panel.Elements.Add(panel3);
			
			panel4 = new FlowPanel();
			panel4.Orientation = FlowPanelOrientation.Horizontal;
			panel4.Rect = new PageRect(0, 0, 250, 45);
			panel4.ElementId = 94;
			panel4.Margins = new Margins(0, 0, 0, 0);
			panel4.HorizontalAlignment = HorizontalAlignment.Left;
			panel4.VerticalAlignment = VerticalAlignment.Top;
			
			button = new TextButton();
			button.PressedColor = new BandColor(0, 121, 214);
			button.Rect = new PageRect(0, 0, 240, 35);
			button.ElementId = 33;
			button.Margins = new Margins(10, 0, 0, 0);
			button.HorizontalAlignment = HorizontalAlignment.Center;
			button.VerticalAlignment = VerticalAlignment.Top;
			
			panel4.Elements.Add(button);
			
			panel.Elements.Add(panel4);
			pageLayout = new PageLayout(panel);
			
			PageElementData[] pageElementDataArray = new PageElementData[3];
			pageElementDataArray[0] = textBlockData;
			pageElementDataArray[1] = textBlock2Data;
			pageElementDataArray[2] = buttonData;
			
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
			await Task.Run(() => { }); // Dealing with CS1998
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
