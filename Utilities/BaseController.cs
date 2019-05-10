using UnityEngine;
using UnityEngine.UI;

public abstract class BaseController : MonoBehaviour
{
    #region Editor Properties
    [SerializeField]
    protected Text statusLabel;
    #endregion

    #region Protected Properties

    private bool isStatusOK;
    protected virtual bool IsStatusOK
    {
        get { return isStatusOK; }
        set
        {
            isStatusOK = value;

            UpdateControlsState();

            UpdateStatusLabel();
        }
    }

    protected abstract string StatusName { get; }
    #endregion

    #region Unity Functions

    protected virtual void Awake()
    {
        RegisterEvents();

        Init();
    }

    protected virtual void Start()
    {
        UpdateControlsState();

        UpdateStatusLabel();
    }
    #endregion

    #region Protected Functions
    protected abstract void Init();

    protected abstract void RegisterEvents();

    protected virtual void UpdateControlsState()
    {
    }

    protected virtual void UpdateStatusLabel()
    {
        statusLabel.text = StatusName + ": " + IsStatusOK;
    }
    #endregion
}