using UnityEngine;

public class HUD : MonoBehaviour
{
	public GUISkin Skin = null;

	// Screen dimensions
	int screenWidth;
	int screenHeight;

	// Misc dimensions
	int panelPadding;

	// Menu buttons dimensions
	int menuButtonsWidth;
	int menuButtonsHeight;
	Rect menuButtonsCanvas;

	// Resource indicators dimensions
	int resourceIndicatorsWidth;
	int resourceIndicatorsHeight;
	Rect resourceIndicatorsCanvas;

	// Minimap panel dimensions
	int minimapWidth;
	int minimapHeight;
	Rect minimapCanvas;
	Rect minimapCanvasWithPadding;

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

	void OnEnable()
	{
		RetrieveGUIDimensions();
	}

	void OnGUI()
	{
		GUI.skin = Skin;

		// Menu buttons
		DrawMenuButtons(menuButtonsCanvas);

		// Resource indicators
		DrawResourceIndicators(resourceIndicatorsCanvas);

		// Minimap panel
		GUI.Box(minimapCanvas, string.Empty);
		DrawMinimap(minimapCanvasWithPadding);

		// Actions panel
		GUI.Box(actionsPanelCanvas, string.Empty);
		DrawActionsPanel(actionsPanelCanvasWithPadding);

		// Unit info panel
		GUI.Box(unitInfoPanelCanvas, string.Empty);
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

		// Resource indicators dimensions
		resourceIndicatorsWidth = screenWidth / 5;
		resourceIndicatorsHeight = screenHeight / 12;
		resourceIndicatorsCanvas = new Rect(screenWidth - resourceIndicatorsWidth, 0, resourceIndicatorsWidth, resourceIndicatorsHeight);

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
		unitInfoPanelHeight = (int)((float)minimapHeight * 0.865f);
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
		GUI.Button(new Rect(canvas.x + buttonPadding + buttonWidth * 0, canvas.y, buttonWidth * 2 - buttonPadding * 2, canvas.height), "Menu (Esc)");
		GUI.Button(new Rect(canvas.x + buttonPadding + buttonWidth * 2, canvas.y, buttonWidth - buttonPadding * 2, canvas.height), "Event Log");
	}

	void DrawResourceIndicators(Rect canvas)
	{
	}

	void DrawMinimap(Rect canvas)
	{
		GUI.Box(canvas, "MINIMAP PLACEHOLDER");
	}

	void DrawActionsPanel(Rect canvas)
	{
	}

	void DrawUnitInfoPanel(Rect canvas)
	{
	}
}
