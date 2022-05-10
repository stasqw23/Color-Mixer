using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mix : MonoBehaviour
{
    [SerializeField] private Button _mix;
    [SerializeField] private GameObject _buttonMix;
    [SerializeField] private GameObject _buttonMixPosition;
    [SerializeField] private Animator _animatorMix;
    [SerializeField] private float _timeMix;
    [SerializeField] private float _timeLid;
    [SerializeField] private Color _verColor;
    [SerializeField] private GameObject _mixLiquid;
    [SerializeField] private Renderer _colorMixLiquid;
    [SerializeField] private Text _texPerc;
    [SerializeField] private float _speedMixUp;
    [SerializeField] private GameObject _coliderMixDestroy;
    [SerializeField] private GameObject _coliderMixDestroyLastPosition;
    [SerializeField] private float _maxValueLiquid;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private GameObject _lid;
    [SerializeField] private GameObject _lidLastPosition;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private Image _progresBarMask;
    [SerializeField] private float _timePause;
    [SerializeField] private int _precForWon;
    [SerializeField] private float _spidPerc;
    [SerializeField] private GameObject _won;
    [SerializeField] private GameObject _lose;



    private List<GameObject> _fruitsList = new List<GameObject>();
    private float _verPerc;
    private bool _flagStartMix = false;
    private bool _flafLid = false;
    private bool _flagResult = false;
    private float _elapseTime;
    void Start()
    {
        _mix.onClick.AddListener(() => MixStart());

    }

    void Update()
    {
        MixRun();
        PauseResults();
    }
    private void MixStart()
    {
        _buttonMix.transform.position = _buttonMixPosition.transform.position;
        CalculationColor();
        _mix.interactable = false;
        StartCoroutine(TimeMix());       


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fruits" )
        {
            _mix.interactable = true;
            _animatorMix.SetTrigger("TrigerFruit");
            _fruitsList.Add(other.gameObject);
        }
    }
    private void CalculationColor()
    {
            float sumR = 0;
            float sumG = 0;
            float sumB = 0;
        for(int i = 0; i < _fruitsList.Count; i++)
        {
            sumR += _fruitsList[i].GetComponent<Fruit>().colorFruit.r;
            sumG += _fruitsList[i].GetComponent<Fruit>().colorFruit.g;
            sumB += _fruitsList[i].GetComponent<Fruit>().colorFruit.b;

        }
         var medR = sumR / _fruitsList.Count ;
         var medG = sumG / _fruitsList.Count ;
         var medB = sumB / _fruitsList.Count ;

        _colorMixLiquid.material.SetColor("LiquidColor", 
            new Color(
                medR,
                medG,
                medB));

        // var perc = Mathf.Round((medR + medG + medB)/3);

        //var verMedR = (medR * 100) / (_verColor.r);
        //var verMedG = (medG * 100) / (_verColor.g);
        //var verMedB = (medB * 100) / (_verColor.b);

        // var verPerc = Mathf.Round((verMedR + verMedG + verMedB)/3);
        var verPerc = Mathf.Sqrt(Mathf.Pow(_verColor.r- medR,2f) + Mathf.Pow(_verColor.g - medG, 2f)+ Mathf.Pow(_verColor.b - medB, 2f));
        //_verPerc = Mathf.Abs(100 - verPerc);
        _verPerc = Mathf.Round(100 - (verPerc * 100));        




    }
    private void MixRun()
    {
        if(_flafLid)
        {
            _lid.transform.position = Vector3.MoveTowards(_lid.transform.position, _lidLastPosition.transform.position, _speedMixUp * Time.deltaTime);
        }

        if (_flagStartMix)
        {           

            _coliderMixDestroy.transform.position = Vector3.MoveTowards(_coliderMixDestroy.transform.position, _coliderMixDestroyLastPosition.transform.position, _speedMixUp * Time.deltaTime);

            var targetFill = Mathf.MoveTowards(_colorMixLiquid.material.GetFloat("Fill"), _maxValueLiquid, _speedMixUp * Time.deltaTime);
            _colorMixLiquid.material.SetFloat("Fill", targetFill);

            _mixLiquid.transform.Rotate(0,_rotateSpeed, 0);

            _elapseTime += Time.deltaTime;
            var totalTimeBar = _elapseTime / _timeMix;
            _progresBarMask.fillAmount = Mathf.MoveTowards(0f, 1f, totalTimeBar);
        }
    }
    private void PauseResults()
    {
        if (_flagResult)
        {
            _elapseTime += Time.deltaTime;
            var totalTimeBar = _elapseTime / _spidPerc;
            int perc = (int) Mathf.Round(Mathf.Lerp(0f, _verPerc, totalTimeBar));
            _texPerc.text = $"{perc}%";
        }
    }
    private IEnumerator TimeMix()
    {
        _flafLid = true;
        _progressBar.SetActive(true);
        yield return new WaitForSeconds(_timeLid);

        _animatorMix.SetTrigger("TrigerMix");
        _flagStartMix = true;

        yield return new WaitForSeconds(_timeMix);


        _flagStartMix = false;
        _flafLid = false;
        _animatorMix.SetTrigger("TrigerEndMix");
        _flagResult = true;
        _elapseTime = 0;

        yield return new WaitForSeconds(_timePause);
        if(_verPerc >= _precForWon)
        {
            _won.SetActive(true);
        }
        else
        {
            _lose.SetActive(true);
        }

        yield return null;
    }
    
    
}
