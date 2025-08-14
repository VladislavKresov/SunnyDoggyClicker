namespace SunnyDoggyClicker.Signals {
    public class ShowViewSignal { public string ViewName; }
    public class HideViewSignal { public string ViewName; }
    public class ClickerButtonUserClick {}
    public class ClickerClick {}
    public class LoadBreed { public string BreedId; }
    public class BreedLoading { public string BreedId; }
    public class BreedLoaded { public DogBreed Breed; }
}