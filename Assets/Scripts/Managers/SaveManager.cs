using Audio;
using EBAC.Core.Singleton;
using Itens;
using Skins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public int debugLevel = 0;

    [SerializeField] private SaveSetup _saveSetup;
    private string _path;
    private string _fileloaded;

    public Action<SaveSetup> fileLoad;

    public SaveSetup SaveSetup
    {
        get { return _saveSetup; }
    }

    private void Init()
    {
        _path = Application.dataPath + "/save.txt";
        
    }

    private void CreatNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLavel = 5;
        _saveSetup.playerName = "Giowany";
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        Init();
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    #region SAVE

    public void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        SaveFile(setupToJson);
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLavel = level;
        SaveItens();
        SaveSkin();
        Save();
    }

    public void SaveItens()
    {
        if(InvetoryManager.instance != null)
        {
            _saveSetup.coins = Itens.InvetoryManager.instance.GetItensForType(Itens.ItenType.COIN).soInt.value;
            _saveSetup.LifePack = Itens.InvetoryManager.instance.GetItensForType(Itens.ItenType.LIFE_PACK).soInt.value;
        }
        Save();
    }

    public void SaveSkin()
    {
        if(ClothManager.instance != null)
            _saveSetup.clothType = Skins.ClothManager.instance.GetSetup().clothType;
        Save();
    }

    public void SaveLastCheckPoint( int lastCheckPoint)
    {
        _saveSetup.lastCheckPoint = lastCheckPoint;
        Save();
    }

    public void SaveVolume()
    {
        _saveSetup.sfxVolume = SoundManager.instance.GetSFXByType(SFXType.COIN).audioClip.volume;
        _saveSetup.musicVolume = SoundManager.instance.GetMusicByType(MusicType.TYPE_01).audioClip.volume;
        _saveSetup.masterVolume = AudioListener.volume;
        _saveSetup.sfxOn = SFXPool.instance.GetActivate();
        _saveSetup.musicOn = MusicPlayer.instance.GetActivate();
        Save();
    }

    public void SaveButton()
    {
        SaveItens();
        SaveSkin();
        SaveVolume();
    }

    #endregion

    private void SaveFile(string json)
    {
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    public void Load()
    {
        if (File.Exists(_path))
        {
            _fileloaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(_fileloaded);
        }

        else
        {
            CreatNewSave();
        }

        fileLoad?.Invoke(_saveSetup);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastCheckPoint;
    public int lastLavel;
    public clothType clothType;
    public int coins;
    public int LifePack;
    public string playerName;
    public float sfxVolume;
    public float musicVolume;
    public float masterVolume;
    public bool sfxOn;
    public bool musicOn;
    public bool masterOn;
}
