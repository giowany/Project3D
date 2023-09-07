using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class NavegationController : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObjectWithController;
    public GameObject selectedObjectWithMouse;

    private bool isUsingController = true;
    private float navigateThreshold = 0.5f; // Limiar para detectar a navega��o

    private Inputs _inputs;

    private void Awake()
    {
        _inputs = new Inputs();
        _inputs.Menu.Enable();
    }

    private void Start()
    {
        // Inicialmente, selecionar o objeto com o controle
        eventSystem.SetSelectedGameObject(selectedObjectWithController);
    }

    private void OnEnable()
    {
        _inputs.Menu.Navigate.started += ctx => Navigate(ctx.ReadValue<Vector2>());
        //_inputs.Menu.ToggleControl.started += ctx => ToggleControl();
    }

    private void OnDisable()
    {
        _inputs.Menu.Navigate.started -= ctx => Navigate(ctx.ReadValue<Vector2>());
        //_inputs.Menu.ToggleControl.started -= ctx => ToggleControl();
    }

    private void Navigate(Vector2 direction)
    {
        if (!isUsingController)
        {
            return; // Apenas processa a navega��o quando estiver usando o controle
        }

        // Verifica a dire��o do controle
        if (direction.x > navigateThreshold)
        {
            NavigateRight();
        }
        else if (direction.x < -navigateThreshold)
        {
            NavigateLeft();
        }
    }

    private void NavigateRight()
    {
        // Desmarque o objeto atualmente selecionado
        eventSystem.SetSelectedGameObject(null);

        // Implemente a l�gica para encontrar o pr�ximo objeto � direita
        GameObject nextObject = FindNextObjectToRight(selectedObjectWithController);

        // Se encontrarmos um pr�ximo objeto, selecione-o
        if (nextObject != null)
        {
            eventSystem.SetSelectedGameObject(nextObject);
        }
    }

    private void NavigateLeft()
    {
        // Desmarque o objeto atualmente selecionado
        eventSystem.SetSelectedGameObject(null);

        // Implemente a l�gica para encontrar o pr�ximo objeto � esquerda
        GameObject nextObject = FindNextObjectToLeft(selectedObjectWithController);

        // Se encontrarmos um pr�ximo objeto, selecione-o
        if (nextObject != null)
        {
            eventSystem.SetSelectedGameObject(nextObject);
        }
    }

    private GameObject FindNextObjectToLeft(GameObject currentObject)
    {
        // Obt�m todos os objetos do menu (voc� pode personalizar isso para a sua estrutura)
        GameObject[] menuItems = GetAllMenuItems();

        if (menuItems == null || menuItems.Length == 0)
        {
            return null;
        }

        int currentIndex = -1;

        // Encontra o �ndice do objeto atualmente selecionado na lista
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (menuItems[i] == currentObject)
            {
                currentIndex = i;
                break;
            }
        }

        if (currentIndex == -1)
        {
            // Objeto atual n�o encontrado na lista
            return null;
        }

        // Calcula o pr�ximo �ndice � esquerda com looping circular
        int nextIndex = (currentIndex - 1 + menuItems.Length) % menuItems.Length;

        // Retorna o pr�ximo objeto � esquerda
        return menuItems[nextIndex];
    }

    private GameObject FindNextObjectToRight(GameObject currentObject)
    {
        // Obt�m todos os objetos do menu (voc� pode personalizar isso para a sua estrutura)
        GameObject[] menuItems = GetAllMenuItems();

        if (menuItems == null || menuItems.Length == 0)
        {
            return null;
        }

        int currentIndex = -1;

        // Encontra o �ndice do objeto atualmente selecionado na lista
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (menuItems[i] == currentObject)
            {
                currentIndex = i;
                break;
            }
        }

        if (currentIndex == -1)
        {
            // Objeto atual n�o encontrado na lista
            return null;
        }

        // Calcula o pr�ximo �ndice � direita com looping circular
        int nextIndex = (currentIndex + 1) % menuItems.Length;

        // Retorna o pr�ximo objeto � direita
        return menuItems[nextIndex];
    }

    private GameObject[] GetAllMenuItems()
    {
        // Suponha que todos os itens do menu sejam filhos de um objeto pai
        // Substitua "MenuParent" pelo nome do objeto pai na hierarquia.
        GameObject menuParent = GameObject.Find("MenuParent");

        if (menuParent == null)
        {
            Debug.LogError("MenuParent n�o encontrado na hierarquia.");
            return null;
        }

        // Obt�m todos os objetos do menu que s�o filhos de MenuParent
        GameObject[] menuItems = menuParent.GetComponentsInChildren<GameObject>();

        if (menuItems == null || menuItems.Length == 0)
        {
            Debug.LogError("Nenhum item de menu encontrado sob MenuParent.");
            return null;
        }

        return menuItems;
    }


    private void ToggleControl()
    {
        isUsingController = !isUsingController;
        if (isUsingController)
        {
            eventSystem.SetSelectedGameObject(selectedObjectWithController);
        }
        else
        {
            eventSystem.SetSelectedGameObject(selectedObjectWithMouse);
        }
    }
}
