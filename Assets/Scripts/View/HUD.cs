using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class HUD : MonoBehaviour
{
	enum UnitPriority
	{
		UnknownPriority = -1,
		BloodCellPriority = 0,
		VirusPriority,
		OrganPriority
	}

	public delegate void ActionButtonPressedAction();
	public delegate void MouseOverGUIAction();
	public event ActionButtonPressedAction OnSpawnRedBloodCellActionButtonPressed;
	public event ActionButtonPressedAction OnSpawnWhiteBloodCellActionButtonPressed;
	public event MouseOverGUIAction OnMouseOverGUI;
	public event MouseOverGUIAction OnMouseOutsideGUI;

	public GUISkin Skin;

	// Screen dimensions
	int screenWidth;
	int screenHeight;

	// Misc dimensions
	int panelPadding;

	// Menu buttons dimensions
	int menuButtonsWidth;
	int menuButtonsHeight;
	Rect menuButtonsCanvas;
	Rect menuButtonsCanvasWithPadding;

	// Resource indicators dimensions
	int resourceIndicatorsWidth;
	int resourceIndicatorsHeight;
	Rect resourceIndicatorsCanvas;
	Rect resourceIndicatorsCanvasWithPadding;

	// Minimap panel dimensions
	int minimapWidth;
	int minimapHeight;
	Rect minimapCanvas;
	Rect minimapCanvasWithPadding;
	RenderTexture minimapRenderTexture;

	// Actions panel dimensions
	int actionsPanelWidth;
	int actionsPanelHeight;
	Rect actionsPanelCanvas;
	Rect actionsPanelCanvasWithPadding;

	// Unit info panel dimensions
	int unitInfoPanelWidth;
	int unitInfoPanelHeight;
	Rect unitInfoPanelCanvas;
	Rect unitInfoPanelCanvasWithPadding;
	Vector2 scrollPosition = Vector2.zero;
	SortedList<UnitPriority, List<Unit>> selectedUnits;

	// Icons
	Texture infectionIcon;
	Texture minerIcon;
	Texture pathogenIcon;
	Texture redBloodCellIcon;
	Texture spawnerIcon;
	Texture sporeIcon;
	Texture whiteBloodCellIcon;

	void OnEnable()
	{
		RetrieveGUIDimensions();
		minimapRenderTexture = Resources.Load(Util.Path.Combine("Textures", "MinimapRenderTexture")) as RenderTexture;
		infectionIcon = Resources.Load(Util.Path.Combine("Textures", "InfectionIcon")) as Texture;
		minerIcon = Resources.Load(Util.Path.Combine("Textures", "MinerIcon")) as Texture;
		pathogenIcon = Resources.Load(Util.Path.Combine("Textures", "PathogenIcon")) as Texture;
		redBloodCellIcon = Resources.Load(Util.Path.Combine("Textures", "RedBloodCellIcon")) as Texture;
		spawnerIcon = Resources.Load(Util.Path.Combine("Textures", "SpawnerIcon")) as Texture;
		sporeIcon = Resources.Load(Util.Path.Combine("Textures", "SporeIcon")) as Texture;
		whiteBloodCellIcon = Resources.Load(Util.Path.Combine("Textures", "WhiteBloodCellIcon")) as Texture;
	}

	void Update()
	{
		var mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
		if (menuButtonsCanvas.Contains(mousePos) ||
			resourceIndicatorsCanvas.Contains(mousePos) ||
			minimapCanvas.Contains(mousePos) ||
			actionsPanelCanvas.Contains(mousePos) ||
			unitInfoPanelCanvas.Contains(mousePos))
			OnMouseOverGUI();
		else
			OnMouseOutsideGUI();
	}

	void OnGUI()
	{
		if (screenWidth != Screen.width || screenHeight != Screen.height)
			RetrieveGUIDimensions();

		GUI.skin = Skin;

		// Menu buttons
		GUI.Box(menuButtonsCanvas, string.Empty);
		DrawMenuButtons(menuButtonsCanvasWithPadding);

		// Resource indicators
		GUI.Box(resourceIndicatorsCanvas, string.Empty);
		DrawResourceIndicators(resourceIndicatorsCanvasWithPadding);

		// Minimap panel
		GUI.Box(minimapCanvas, string.Empty);
		DrawMinimap(minimapCanvasWithPadding);

		// Actions panel
		GUI.Box(actionsPanelCanvas, "Actions");
		DrawActionsPanel(actionsPanelCanvasWithPadding);

		// Unit info panel
		GUI.Box(unitInfoPanelCanvas, "Details");
		DrawUnitInfoPanel(unitInfoPanelCanvasWithPadding);
	}

	// Sets all the dimensions of HUD components
	// The sequence of assignments in this function are order sensitive, so careful when rearranging
	void RetrieveGUIDimensions()
	{
		// Screen dimensions
		screenWidth = Screen.width;
		screenHeight = Screen.height;

		// Misc dimensions
		panelPadding = Mathf.Min(screenWidth, screenHeight) / 64;

		// Menu buttons dimensions
		menuButtonsWidth = screenWidth / 5;
		menuButtonsHeight = screenHeight / 24;
		menuButtonsCanvas = new Rect(0, 0, menuButtonsWidth, menuButtonsHeight);
		menuButtonsCanvasWithPadding = ApplyPadding(menuButtonsCanvas, panelPadding / 2);

		// Resource indicators dimensions
		resourceIndicatorsWidth = screenWidth / 5;
		resourceIndicatorsHeight = screenHeight / 12;
		resourceIndicatorsCanvas = new Rect(screenWidth - resourceIndicatorsWidth, 0, resourceIndicatorsWidth, resourceIndicatorsHeight);
		resourceIndicatorsCanvasWithPadding = ApplyPadding(resourceIndicatorsCanvas, panelPadding / 2);

		// Minimap panel dimensions
		minimapWidth = screenWidth / 6;
		minimapHeight = minimapWidth;
		minimapCanvas = new Rect(0, screenHeight - minimapHeight, minimapWidth, minimapHeight);
		minimapCanvasWithPadding = ApplyPadding(minimapCanvas, panelPadding);

		// Actions panel dimensions
		actionsPanelWidth = minimapWidth;
		actionsPanelHeight = actionsPanelWidth;
		actionsPanelCanvas = new Rect(screenWidth - actionsPanelWidth, screenHeight - actionsPanelHeight, actionsPanelWidth, actionsPanelHeight);
		actionsPanelCanvasWithPadding = ApplyPadding(actionsPanelCanvas, panelPadding);

		// Unit info panel dimensions
		unitInfoPanelWidth = screenWidth - minimapWidth - actionsPanelWidth;
		unitInfoPanelHeight = (int)(minimapHeight * 0.865f);
		unitInfoPanelCanvas = new Rect(minimapWidth, screenHeight - unitInfoPanelHeight, unitInfoPanelWidth, unitInfoPanelHeight);
		unitInfoPanelCanvasWithPadding = ApplyPadding(unitInfoPanelCanvas, panelPadding);
	}

	Rect ApplyPadding(Rect canvas, int padding)
	{
		return new Rect(canvas.x + padding, canvas.y + padding, canvas.width - 2 * padding, canvas.height - 2 * padding);
	}

	void DrawMenuButtons(Rect canvas)
	{
		int nButtons = 2;
		// This is to give the menu button a larger size than the rest of the buttons
		int buttonWidth = (int)canvas.width / (nButtons + 1);
		int buttonPadding = (int)canvas.width / 64;
		if (GUI.Button(new Rect(canvas.x + buttonPadding + buttonWidth * 0, canvas.y, buttonWidth * 2 - buttonPadding * 2, canvas.height), "Menu (Esc)"))
			Application.Quit();
		GUI.Button(new Rect(canvas.x + buttonPadding + buttonWidth * 2, canvas.y, buttonWidth - buttonPadding * 2, canvas.height), "Event Log");
	}

	void DrawResourceIndicators(Rect canvas)
	{
	}

	void DrawMinimap(Rect canvas)
	{
		GUI.DrawTexture(canvas, minimapRenderTexture);
	}

	void DrawActionsPanel(Rect canvas)
	{
		var selected = new List<Unit>();
		if (selectedUnits != null)
			foreach (var p in selectedUnits)
				selected.AddRange(p.Value);
		if (selected.Count == 1)
		{
			int buttonWidth = (int)canvas.width / 3;
			int buttonHeight = buttonWidth;
			int buttonPadding = (int)canvas.width / 64;
			if (selected[0] is Spawner)
			{
				if (GUI.Button(new Rect(canvas.x + buttonPadding + buttonWidth * 0, canvas.y + buttonPadding + Skin.font.fontSize, buttonWidth - buttonPadding, buttonHeight - buttonPadding), redBloodCellIcon))
				{
					if (OnSpawnRedBloodCellActionButtonPressed != null)
						OnSpawnRedBloodCellActionButtonPressed();
				}
				if (GUI.Button(new Rect(canvas.x + buttonPadding + buttonWidth * 1, canvas.y + buttonPadding + Skin.font.fontSize, buttonWidth - buttonPadding, buttonHeight - buttonPadding), whiteBloodCellIcon))
				{
					if (OnSpawnWhiteBloodCellActionButtonPressed != null)
						OnSpawnWhiteBloodCellActionButtonPressed();
				}
			}
		}
	}

	void DrawUnitInfoPanel(Rect canvas)
	{
		var can = new Rect(canvas.x, canvas.y + Skin.font.fontSize, canvas.width, canvas.height - Skin.font.fontSize);
		var scrollArea = new Rect(0.0f, 0.0f, can.width - 20.0f, can.height);
		int iconsPerRow = 4;
		int iconPadding = (int)scrollArea.width / 64;
		int iconWidth = ((int)scrollArea.width - iconPadding) / iconsPerRow;
		int iconHeight = iconWidth / 2;
		int scrollAreaHeight = iconPadding;
		if (selectedUnits != null)
		{
			foreach (var p in selectedUnits)
				scrollAreaHeight += iconHeight * (int)Mathf.Ceil((float)p.Value.Count / iconsPerRow);
		}
		scrollArea.height = Mathf.Max(scrollArea.height, scrollAreaHeight);
		scrollPosition = GUI.BeginScrollView(can, scrollPosition, scrollArea);
		GUI.Box(scrollArea, string.Empty);
		if (selectedUnits != null)
		{
			int j = 0;
			foreach (var p in selectedUnits)
			{
				int i = 0;
				bool newRow = false;
				foreach (Unit u in p.Value)
				{
					newRow = false;
					var title = u.GetTypeName();
					Texture t = null;
					var info = u.GetStatsInfo();
					if (title == "Heart")
						t = spawnerIcon;
					if (title == "Infection")
						t = infectionIcon;
					if (title == "Left Kidney" || title == "Right Kidney")
						t = minerIcon;
					if (title == "Lungs")
						t = minerIcon;
					if (title == "Pathogen")
						t = pathogenIcon;
					if (title == "Red Blood Cell")
						t = redBloodCellIcon;
					if (title == "Spore")
						t = sporeIcon;
					if (title == "Stomach")
						t = minerIcon;
					if (title == "White Blood Cell")
						t = whiteBloodCellIcon;
					var unitInfoCanvas = new Rect(iconPadding + iconWidth * (i % iconsPerRow), iconPadding + iconHeight * j, iconWidth - iconPadding, iconHeight - iconPadding);
					var unitTexture = new Rect(unitInfoCanvas.x + iconPadding / 2, unitInfoCanvas.y + iconPadding / 2 + Skin.font.fontSize, unitInfoCanvas.width / 2 - iconPadding, unitInfoCanvas.height - iconPadding - Skin.font.fontSize);
					unitTexture.width = unitTexture.height = Mathf.Min(unitTexture.width, unitTexture.height);
					var unitInfoBox = new Rect(unitTexture.xMax + iconPadding / 2, unitTexture.y, unitInfoCanvas.xMax - unitTexture.xMax - iconPadding, unitTexture.height);
					GUI.Box(unitInfoCanvas, title);
					GUI.DrawTexture(unitTexture, t);
					GUI.TextArea(unitInfoBox, info.ToString());
					i++;
					if (i % iconsPerRow == 0)
					{
						j++;
						newRow = true;
					}
				}
				if (!newRow && p.Value.Count > 0)
					j++;
			}
		}
		GUI.EndScrollView();
	}

	public void UpdateInfoPanel(HashSet<Unit> selected)
	{
		selectedUnits = new SortedList<UnitPriority, List<Unit>>();
		selectedUnits[UnitPriority.BloodCellPriority] = new List<Unit>();
		selectedUnits[UnitPriority.VirusPriority] = new List<Unit>();
		selectedUnits[UnitPriority.OrganPriority] = new List<Unit>();
		foreach (Unit u in selected)
		{
			var priority = GetUnitPriority(u);
			if (selectedUnits.ContainsKey(priority))
				selectedUnits[priority].Add(u);
		}
	}

	UnitPriority GetUnitPriority(Unit u)
	{
		if (u is BloodCell) return UnitPriority.BloodCellPriority;
		if (u is Virus) return UnitPriority.VirusPriority;
		if (u is Organ) return UnitPriority.OrganPriority;
		return UnitPriority.UnknownPriority;
	}
}
