namespace Mobile.Features.Challenges;

public sealed class ChallengeTemplateSelector : DataTemplateSelector
{
    public DataTemplate CompletedTemplate { get; set; }
    public DataTemplate PendingTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is not ChallengeViewModel challenge)
        {
            return null;
        }

        return challenge.Completed ? CompletedTemplate : PendingTemplate;
    }
}
