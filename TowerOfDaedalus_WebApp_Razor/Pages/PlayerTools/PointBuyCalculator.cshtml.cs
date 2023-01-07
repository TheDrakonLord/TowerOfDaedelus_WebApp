using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace TowerOfDaedelus_WebApp.Pages.PlayerTools
{
    /// <summary>
    /// a page that allows users to calculate ability scores using a modified point-buy model
    /// TODO: convert this page to a blazor page instead
    /// </summary>
    //[Authorize(Policy = "allPlayers")]
    [AllowAnonymous]
    public class PointBuyCalculatorModel : PageModel
    {
        /// <summary>
        /// constant string representing the mind ability score
        /// </summary>
        public const string SessionKeyIntMind = "_IntMind";

        /// <summary>
        /// constant string representing the strength ability score
        /// </summary>
        public const string SessionKeyIntStrength = "_IntStrength";

        /// <summary>
        /// constant string representing the agility ability score
        /// </summary>
        public const string SessionKeyIntAgility = "_IntAgility";

        /// <summary>
        /// constant string representing the constitution ability score
        /// </summary>
        public const string SessionKeyIntConstitution = "_IntConstitution";

        /// <summary>
        /// constant string representing the soul ability score
        /// </summary>
        public const string SessionKeyIntSoul = "_IntSoul";

        /// <summary>
        /// constant string representing the total points a user has
        /// </summary>
        public const string SessionKeyIntPoints = "_IntPoints";


        private const int MaxPoints = 15;
        private const int MinPoints = 0;

        private const int MaxAssigned = 7;
        private const int MinAssigned = -2;

        private readonly int[] PointCosts = {-2,-1, 0, 1, 2, 3, 4, 5, 7, 9};

        private const int PointArrayAdjustment = 2;

        private const int DefaultValue = 0;

        /// <summary>
        /// the current value of the mind ability score
        /// </summary>
        public int IntMind { get; set; }

        /// <summary>
        /// the current value of the strength ability score
        /// </summary>
        public int IntStrength { get; set; }

        /// <summary>
        /// the current value of the agility ability score
        /// </summary>
        public int IntAgility { get; set; }

        /// <summary>
        /// the current value of the constitution ability score
        /// </summary>
        public int IntConstitution { get; set; }

        /// <summary>
        /// the current value of the soul ability score
        /// </summary>
        public int IntSoul { get; set; }

        /// <summary>
        /// the current amount of points the user has to spend
        /// </summary>
        public int IntPoints { get; set; }

        private readonly ILogger<PointBuyCalculatorModel> _logger;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="logger">the logger used to log messages</param>
        public PointBuyCalculatorModel(ILogger<PointBuyCalculatorModel> logger)
        {
            _logger = logger;
        }

        private int adjustPoints(int stat, bool decrease)
        {
            int adjustment = 1;
            
            if (decrease)
            {
                adjustment = -1;
            }

            if ((decrease && stat != MinAssigned) || (!decrease && stat != MaxAssigned))
            {
                int cost = -1 * (PointCosts[stat + adjustment + PointArrayAdjustment] - PointCosts[stat + PointArrayAdjustment]);

                if (IntPoints + cost >= MinPoints && stat + adjustment >= MinAssigned && stat + adjustment <= MaxAssigned)
                {
                    IntPoints += cost;
                    stat += adjustment;
                }
                HttpContext.Session.SetInt32(SessionKeyIntPoints, IntPoints);
                _logger.LogInformation("Session IntPoints: {IntPoints}", IntPoints);
            }
            return stat;
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPost");
            return Page();
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved from the mind up button
        /// </summary>
        /// <returns>a PageResult object that renders the page</returns>
        public IActionResult OnPostMndUp()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPostMndUp");
            refreshSessionKeys();
            int result = adjustPoints(IntMind, false);
            HttpContext.Session.SetInt32(SessionKeyIntMind, result);
            IntMind = result;
            _logger.LogInformation("Session IntMind: {IntMind}", IntMind);
            return Page();
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved from the mind down button
        /// </summary>
        /// <returns>a PageResult object that renders the page</returns>
        public IActionResult OnPostMndDown()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPostMndDown");
            refreshSessionKeys();
            int result = adjustPoints(IntMind, true);
            HttpContext.Session.SetInt32(SessionKeyIntMind, result);
            _logger.LogInformation("Session IntMind: {IntMind}", IntMind);
            IntMind = result;
            return Page();
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved from the strength up button
        /// </summary>
        /// <returns>a PageResult object that renders the page</returns>
        public IActionResult OnPostStrUp()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPostStrUp");
            refreshSessionKeys();
            int result = adjustPoints(IntStrength, false);
            HttpContext.Session.SetInt32(SessionKeyIntStrength, result);
            IntStrength = result;
            _logger.LogInformation("Session IntStrength: {IntStrength}", IntStrength);
            return Page();
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved from the strength down button
        /// </summary>
        /// <returns>a PageResult object that renders the page</returns>
        public IActionResult OnPostStrDown()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPostStrDown");
            refreshSessionKeys();
            int result = adjustPoints(IntStrength, true);
            HttpContext.Session.SetInt32(SessionKeyIntStrength, result);
            IntStrength= result;
            _logger.LogInformation("Session IntStrength: {IntStrength}", IntStrength);
            return Page();
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved from the agility up button
        /// </summary>
        /// <returns>a PageResult object that renders the page</returns>
        public IActionResult OnPostAglUp()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPostAglUp");
            refreshSessionKeys();
            int result = adjustPoints(IntAgility, false);
            HttpContext.Session.SetInt32(SessionKeyIntAgility, result);
            IntAgility = result;
            _logger.LogInformation("Session IntAgility: {IntAgility}", IntAgility);
            return Page();
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved from the agility down button
        /// </summary>
        /// <returns>a PageResult object that renders the page</returns>
        public IActionResult OnPostAglDown()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPostAglDown");
            refreshSessionKeys();
            int result = adjustPoints(IntAgility, true);
            HttpContext.Session.SetInt32(SessionKeyIntAgility, result);
            IntAgility = result;
            _logger.LogInformation("Session IntAgility: {IntAgility}", IntAgility);
            return Page();
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved from the constitution up button
        /// </summary>
        /// <returns>a PageResult object that renders the page</returns>
        public IActionResult OnPostConUp()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPostConUp");
            refreshSessionKeys();
            int result = adjustPoints(IntConstitution, false);
            HttpContext.Session.SetInt32(SessionKeyIntConstitution, result);
            IntConstitution = result;
            _logger.LogInformation("Session IntConstitution: {IntConstitution}", IntConstitution);
            return Page();
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved from the constitution down button
        /// </summary>
        /// <returns>a PageResult object that renders the page</returns>
        public IActionResult OnPostConDown()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPostConDown");
            refreshSessionKeys();
            int result = adjustPoints(IntConstitution, true);
            HttpContext.Session.SetInt32(SessionKeyIntConstitution, result);
            IntConstitution = result;
            _logger.LogInformation("Session IntConstitution: {IntConstitution}", IntConstitution);
            return Page();
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved from the soul up button
        /// </summary>
        /// <returns>a PageResult object that renders the page</returns>
        public IActionResult OnPostSolUp()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPostSolUp");
            refreshSessionKeys();
            int result = adjustPoints(IntSoul, false);
            HttpContext.Session.SetInt32(SessionKeyIntSoul, result);
            IntSoul = result;
            _logger.LogInformation("Session IntSoul: {IntSoul}", IntSoul);
            return Page();
        }

        /// <summary>
        /// method that is executed any time a POST request is recieved from the soul down button
        /// </summary>
        /// <returns>a PageResult object that renders the page</returns>
        public IActionResult OnPostSolDown()
        {
            _logger.LogInformation("Event Fired: {Event}", "OnPostSolDown");
            refreshSessionKeys();
            int result = adjustPoints(IntSoul, true);
            HttpContext.Session.SetInt32(SessionKeyIntSoul, result);
            IntSoul = result;
            _logger.LogInformation("Session IntSoul: {IntSoul}", IntSoul);
            return Page();
        }

        private void initializeSessionkey(string keyname)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString(keyname)))
            {
                HttpContext.Session.SetInt32(keyname, DefaultValue);
            }
        }

        private void refreshSessionKeys()
        {
            IntMind = (int)HttpContext.Session.GetInt32(SessionKeyIntMind);
            _logger.LogInformation("Session IntMind: {IntMind}", IntMind);

            IntStrength = (int)HttpContext.Session.GetInt32(SessionKeyIntStrength);
            _logger.LogInformation("Session IntStrength: {IntStrength}", IntStrength);

            IntAgility = (int)HttpContext.Session.GetInt32(SessionKeyIntAgility);
            _logger.LogInformation("Session IntAgility: {IntAgility}", IntAgility);

            IntConstitution = (int)HttpContext.Session.GetInt32(SessionKeyIntConstitution);
            _logger.LogInformation("Session IntConstitution: {IntConstitution}", IntConstitution);

            IntSoul = (int)HttpContext.Session.GetInt32(SessionKeyIntSoul);
            _logger.LogInformation("Session IntSoul: {IntSoul}", IntSoul);

            IntPoints = (int)HttpContext.Session.GetInt32(SessionKeyIntPoints);
            _logger.LogInformation("Session IntPoints: {IntPoints}", IntPoints);
        }

        /// <summary>
        /// method that is executed any time a GET request is recieived
        /// populates all fields on the page with their values
        /// </summary>
        public void OnGet()
        {
            initializeSessionkey(SessionKeyIntMind);
            IntMind = (int)HttpContext.Session.GetInt32(SessionKeyIntMind);
            _logger.LogInformation("Session IntMind: {IntMind}", IntMind);

            initializeSessionkey(SessionKeyIntStrength);
            IntStrength = (int)HttpContext.Session.GetInt32(SessionKeyIntStrength);
            _logger.LogInformation("Session IntStrength: {IntStrength}", IntStrength);

            initializeSessionkey(SessionKeyIntAgility);
            IntAgility = (int)HttpContext.Session.GetInt32(SessionKeyIntAgility);
            _logger.LogInformation("Session IntAgility: {IntAgility}", IntAgility);

            initializeSessionkey(SessionKeyIntConstitution);
            IntConstitution = (int)HttpContext.Session.GetInt32(SessionKeyIntConstitution);
            _logger.LogInformation("Session IntConstitution: {IntConstitution}", IntConstitution);

            initializeSessionkey(SessionKeyIntSoul);
            IntSoul = (int)HttpContext.Session.GetInt32(SessionKeyIntSoul);
            _logger.LogInformation("Session IntSoul: {IntSoul}", IntSoul);

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyIntPoints)))
            {
                HttpContext.Session.SetInt32(SessionKeyIntPoints, MaxPoints);
            }
            IntPoints = (int)HttpContext.Session.GetInt32(SessionKeyIntPoints);
            _logger.LogInformation("Session IntPoints: {IntPoints}", IntPoints);
        }
    }
}
