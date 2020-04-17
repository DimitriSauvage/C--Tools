namespace Tools.Api.OData.Filtering.Functions.Abstractions
{
    public abstract class ODataLogicalFunction : ODataFunction
    {
        #region Constants

        #endregion

        #region Properties

        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public ODataLogicalFunction(string name) : base(name)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Récupère la représentation sous forme d'URL de la fonction
        /// </summary>
        /// <returns></returns>
        public override string GetUrlRepresentation()
        {
            return $" {this.Name} ";
        }
        #endregion
    }
}
